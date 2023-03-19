namespace WebAPI.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.DTOs.Auth;
    using WebAPI.DTOs.Enums;
    using WebAPI.Infrastructure.DTOs;
    using WebAPI.Models;
    using WebAPI.Services.BusinessLogic.Auth;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService authService;

        public UserController(IUserService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult UsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return this.Ok(new RequestResultDTO
                {
                    Message = "Username is required!",
                    DangerLevel = DangerLevel.Warning,
                });
            }

            var result = this.authService.UsernameExists(username);

            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterInputDTO userData)
        {
            var validationResult = userData.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.Ok(validationResult);
            }

            var result = await this.authService.RegisterUserAsync(userData);

            if (result.IsSuccessful)
            {
                string token = this.authService.CreateJWTToken(result.Data);

                return this.Ok(new RequestResultDTO<string>
                {
                    Data = token,
                    IsSuccessful = true,
                    Message = result.Message,
                });
            }

            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInputDTO userData)
        {
            var validationResult = userData.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.Ok(validationResult);
            }

            var result = await this.authService.LoginUserAsync(userData);

            if (result.IsSuccessful)
            {
                string token = this.authService.CreateJWTToken(result.Data);

                return this.Ok(new RequestResultDTO<string>
                {
                    Data = token,
                    IsSuccessful = true,
                });
            }

            return this.Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailInputDTO input)
        {
            var validationResult = input.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.Ok(validationResult);
            }

            var sid = (this.User.Identity as ClaimsIdentity).FindFirst(JwtRegisteredClaimNames.Sid);

            if (sid == null)
            {
                return this.Ok(new RequestResultDTO
                {
                    IsSuccessful = false,
                    DangerLevel = DangerLevel.Danger,
                    Message = "Problem with sid occured! Report to developers!",
                });
            }

            var result = await this.authService.ConfirmEmailAsync(sid.Value, input.Code);

            return this.Ok(result);
        }
    }
}
