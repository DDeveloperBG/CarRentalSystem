namespace WebAPI.Infrastructure.Extension
{
    using WebAPI.Common;

    public static class ConfigureConfiguration
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(GlobalConstants.ConfigurationKeys.DbConnectionStringKey);
        }

        public static string GetClientUrl(this IConfiguration configuration)
        {
            return configuration[GlobalConstants.ConfigurationKeys.ClientUrlKey];
        }
    }
}
