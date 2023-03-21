namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WebAPI.DTOs.Auth;
    using WebAPI.DTOs.Enums;
    using WebAPI.Models;
    using WebAPI.Services.BusinessLogic.Auth;

    public class UserController : BaseApiController
    {
        private readonly IUserBusinessLogicService authService;

        public UserController(IUserBusinessLogicService authService)
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
            var sid = this.GetUserId();

            var result = await this.authService.ConfirmEmailAsync(sid, input.Code);

            return this.Ok(result);
        }
    }
}
