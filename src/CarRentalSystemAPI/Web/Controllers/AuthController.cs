namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.DTOs.Auth;
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
    }
}
