namespace WebAPI.Services.BusinessLogic
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WebAPI.Common;
    using WebAPI.DTOs.Auth;
    using WebAPI.DTOs.Gmail;
    using WebAPI.Services.BusinessLogic.Auth;
    using WebAPI.Services.BusinessLogic.EmailSender;
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
            serviceCollection.AddTransient<IAuthService, AuthService>();
        }
    }
}
