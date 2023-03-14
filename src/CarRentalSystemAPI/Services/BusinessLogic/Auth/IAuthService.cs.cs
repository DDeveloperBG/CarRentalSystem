namespace WebAPI.Services.BusinessLogic.Auth
{
    using WebAPI.DTOs.Auth;
    using WebAPI.Models;

    public interface IAuthService
    {
        Task<RequestResultDTO<UserDTO>> RegisterUserAsync(RegisterInputDTO userData);

        Task<RequestResultDTO<UserDTO>> LoginUserAsync(LoginInputDTO userData);

        string CreateJWTToken(UserDTO user);
    }
}
