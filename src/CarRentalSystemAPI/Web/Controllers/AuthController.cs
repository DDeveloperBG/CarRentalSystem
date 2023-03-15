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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterInputDTO userData)
        {
            var validationResult = userData.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.BadRequest(validationResult);
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

            return this.BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInputDTO userData)
        {
            var validationResult = userData.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.BadRequest(validationResult);
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

            return this.BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailInputDTO input)
        {
            var validationResult = input.ValidateInput(this.ModelState);

            if (!validationResult.IsSuccessful)
            {
                return this.BadRequest(validationResult);
            }

            var sid = (this.User.Identity as ClaimsIdentity).FindFirst(JwtRegisteredClaimNames.Sid);

            if (sid == null)
            {
                return this.BadRequest(new RequestResultDTO
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
