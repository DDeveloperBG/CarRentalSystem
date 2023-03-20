namespace WebAPI.Services.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using WebAPI.Services.Data.User;
    using WebAPI.Services.Data.Car;

    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ICarService, CarService>();
        }
    }
}
