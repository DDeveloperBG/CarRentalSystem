namespace WebAPI.Services.BusinessLogic.Url
{
    using Microsoft.Extensions.Configuration;
    using WebAPI.Common;

    public class UrlService : IUrlService
    {
        private readonly string clientUrl;

        public UrlService(IConfiguration configuration)
        {
            this.clientUrl = configuration[GlobalConstants.ConfigurationKeys.ClientUrlKey];
        }

        public string GetClientUrl(string path, string parameters)
        {
            return $"{this.clientUrl}/{path}?{parameters}";
        }
    }
}
