namespace WebAPI.Services.BusinessLogic
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WebAPI.Common;
    using WebAPI.DTOs.Auth;
    using WebAPI.DTOs.Gmail;
    using WebAPI.DTOs.Storage;
    using WebAPI.Services.BusinessLogic.Auth;
    using WebAPI.Services.BusinessLogic.Car;
    using WebAPI.Services.BusinessLogic.CloudStorage;
    using WebAPI.Services.BusinessLogic.EmailSender;
    using WebAPI.Services.BusinessLogic.Image;
    using WebAPI.Services.BusinessLogic.Time;
    using WebAPI.Services.BusinessLogic.Url;

    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AddSingletonServices(serviceCollection, configuration);

            AddTransientServices(serviceCollection);
        }

        private static void AddSingletonServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(_ => new CloudinaryCofigKeys
            {
                ApiKey = configuration[GlobalConstants.ConfigurationKeys.Cloudinary.ApiKey],
                ApiSecret = configuration[GlobalConstants.ConfigurationKeys.Cloudinary.ApiSecret],
                CloudName = configuration[GlobalConstants.ConfigurationKeys.Cloudinary.CloudName],
            });

            serviceCollection.AddSingleton(_ => new JWTSettings
            {
                Issuer = configuration[GlobalConstants.ConfigurationKeys.JWT.IssuerKey],
                Audience = configuration[GlobalConstants.ConfigurationKeys.JWT.AudienceKey],
                Secret = configuration[GlobalConstants.ConfigurationKeys.JWT.SecretKey],
            });

            serviceCollection.AddSingleton(_ => new GmailSenderCofigKeys
            {
                Email = configuration[GlobalConstants.ConfigurationKeys.Gmail.Email],
                Password = configuration[GlobalConstants.ConfigurationKeys.Gmail.Password],
            });
        }

        private static void AddTransientServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailSenderService, GmailEmailSender>();
            serviceCollection.AddTransient<IUrlService, UrlService>();
            serviceCollection.AddTransient<ICloudStorageService, CloudinaryService>();
            serviceCollection.AddTransient<IImageService, ImageService>();
            serviceCollection.AddTransient<IDateTimeService, DateTimeService>();

            serviceCollection.AddTransient<IUserBusinessLogicService, UserBusinessLogicService>();
            serviceCollection.AddTransient<ICarBusinessLogicService, CarBusinessLogicService>();
        }
    }
}
