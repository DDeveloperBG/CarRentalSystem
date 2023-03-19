namespace WebAPI.Services.Data.User
{
    public interface IUserService
    {
        bool PINExists(string pin);

        bool UsernameExists(string username);
    }
}
