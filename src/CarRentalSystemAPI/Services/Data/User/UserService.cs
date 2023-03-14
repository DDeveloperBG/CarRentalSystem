namespace WebAPI.Services.Data.User
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool PINExists(string pin)
        {
            return this.userRepository
                .AllAsNoTracking()
                .Any(x => x.PersonalIdentificationNumber == pin);
        }
    }
}
