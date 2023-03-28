namespace WebAPI.Services.Data
{
    using Microsoft.Extensions.DependencyInjection;

    using WebAPI.Services.Data.Car.Advertisement;
    using WebAPI.Services.Data.Car.Image;
    using WebAPI.Services.Data.Car.RentingRequest;
    using WebAPI.Services.Data.User;

    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ICarAdvertisementService, CarAdvertisementService>();
            serviceCollection.AddTransient<ICarRentingRequestService, CarRentingRequestService>();
            serviceCollection.AddTransient<ICarImageService, CarImageService>();
            serviceCollection.AddTransient<IPickupLocationService, PickupLocationService>();
        }
    }
}
