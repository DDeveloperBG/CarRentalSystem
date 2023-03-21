namespace WebAPI.Services.BusinessLogic.Image
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task<Stream> ProcessAsync(Stream image, int width);
    }
}
