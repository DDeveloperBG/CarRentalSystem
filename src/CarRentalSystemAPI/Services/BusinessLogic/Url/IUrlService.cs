namespace WebAPI.Services.BusinessLogic.Url
{
    public interface IUrlService
    {
        string GetClientUrl(string path, string parameters);
    }
}
