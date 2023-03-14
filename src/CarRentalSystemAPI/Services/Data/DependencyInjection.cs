namespace WebAPI.Services.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using WebAPI.Services.Data.User;

    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
