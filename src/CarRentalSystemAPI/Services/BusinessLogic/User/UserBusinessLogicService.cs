namespace WebAPI.Services.BusinessLogic.Auth
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using WebAPI.Common;
    using WebAPI.Data.Models;
    using WebAPI.DTOs.Auth;
    using WebAPI.DTOs.Enums;
    using WebAPI.Models;
    using WebAPI.Services.BusinessLogic.EmailSender;
    using WebAPI.Services.BusinessLogic.Url;
    using WebAPI.Services.Data.User;

    public class UserBusinessLogicService : IUserBusinessLogicService
    {
        private readonly JWTSettings jwtSettings;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IEmailSenderService emailSender;
        private readonly ILogger<UserBusinessLogicService> logger;
        private readonly IUrlService urlService;
        private readonly IUserService userService;

        public UserBusinessLogicService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<UserBusinessLogicService> logger,
            IEmailSenderService emailSender,
            IUrlService urlService,
            IUserService userService,
            JWTSettings jwtSettings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userStore = userStore;
            this.logger = logger;
            this.emailSender = emailSender;
            this.urlService = urlService;
            this.userService = userService;
            this.jwtSettings = jwtSettings;
        }

        public async Task<RequestResultDTO<UserDTO>> RegisterUserAsync(RegisterInputDTO userData)
        {
            if (this.userService.PINExists(userData.PersonalIdentificationNumber))
            {
                return new RequestResultDTO<UserDTO>
                {
                    IsSuccessful = false,
                    Message = "User with this PIN already exists!",
                    DangerLevel = DangerLevel.Warning,
                };
            }

            var user = this.CreateUser(userData);

            await this.userStore.SetUserNameAsync(user, userData.Username, CancellationToken.None);
            var result = await this.userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.Roles.UserRoleName);

                this.logger.LogInformation("User created a new account with password.");

                var userId = await this.userManager.GetUserIdAsync(user);

                var successResult = new RequestResultDTO<UserDTO>
                {
                    IsSuccessful = true,
                    Data = new UserDTO
                    {
                        Id = userId,
                        Username = userData.Username,
                        Roles = new string[] { GlobalConstants.Roles.UserRoleName },
                    },
                };

                if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = this.urlService.GetClientUrl(
                        "/Account/ConfirmEmail", $"code={code}");

                    this.emailSender.SendEmail(
                        userData.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    successResult.Message = "Confirm your email!";

                    return successResult;
                }
                else
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);

                    return successResult;
                }
            }

            return new RequestResultDTO<UserDTO>
            {
                IsSuccessful = false,
                Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)),
                DangerLevel = DangerLevel.Warning,
            };
        }

        public async Task<RequestResultDTO<UserDTO>> LoginUserAsync(LoginInputDTO userData)
        {
            var result = await this.signInManager.PasswordSignInAsync(
                userData.Username,
                userData.Password,
                true,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in.");

                var user = await this.userManager.FindByNameAsync(userData.Username);

                return new RequestResultDTO<UserDTO>
                {
                    IsSuccessful = true,
                    Data = new UserDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Roles = (await this.userManager.GetRolesAsync(user)).ToArray(),
                    },
                };
            }

            string message;
            if (result.IsLockedOut)
            {
                message = "User account locked out.";
            }
            else
            {
                message = "Invalid login attempt.";
            }

            this.logger.LogWarning(message);

            return new RequestResultDTO<UserDTO>
            {
                IsSuccessful = false,
                Message = message,
                DangerLevel = DangerLevel.Danger,
            };
        }

        public string CreateJWTToken(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (user.Roles != null)
            {
                claims.AddRange(user.Roles.Select(x =>
                    new Claim(ClaimTypes.Role, x)));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = this.jwtSettings.Audience,
                Issuer = this.jwtSettings.Issuer,
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<RequestResultDTO> ConfirmEmailAsync(string userId, string code)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new RequestResultDTO
                {
                    IsSuccessful = false,
                    Message = $"DB ERROR! Unable to load user with ID '{userId}'.",
                    DangerLevel = DangerLevel.Danger,
                };
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return new RequestResultDTO(true);
            }

            return new RequestResultDTO
            {
                IsSuccessful = false,
                Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)),
                DangerLevel = DangerLevel.Danger,
            };
        }

        public RequestResultDTO<bool> UsernameExists(string username)
        {
            try
            {
                return new RequestResultDTO<bool>
                {
                    IsSuccessful = true,
                    Data = this.userService.UsernameExists(username),
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDTO<bool>
                {
                    Message = ex.Message,
                    DangerLevel = DangerLevel.Danger,
                };
            }
        }

        private ApplicationUser CreateUser(RegisterInputDTO userData)
        {
            try
            {
                var user = Activator.CreateInstance<ApplicationUser>();

                user.Email = userData.Email;
                user.Forename = userData.Forename;
                user.Surname = userData.Surname;
                user.PersonalIdentificationNumber = userData.PersonalIdentificationNumber;
                user.PhoneNumber = userData.PhoneNumber;

                return user;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
