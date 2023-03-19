namespace WebAPI.Services.BusinessLogic.Auth
{
    using WebAPI.DTOs.Auth;
    using WebAPI.Models;

    public interface IUserService
    {
        Task<RequestResultDTO<UserDTO>> RegisterUserAsync(RegisterInputDTO userData);

        Task<RequestResultDTO<UserDTO>> LoginUserAsync(LoginInputDTO userData);

        Task<RequestResultDTO> ConfirmEmailAsync(string userId, string code);

        RequestResultDTO<bool> UsernameExists(string username);

        string CreateJWTToken(UserDTO user);
    }
}
