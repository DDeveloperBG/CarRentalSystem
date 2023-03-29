namespace WebAPI.Services.Data.Car.Image
{
    using WebAPI.Data.Models.Car;

    public interface ICarImageService
    {
        Task DeleteOneAsync(string imgId);

        Task<string> AddNewAsync(string imgUrl);

        CarImage GetOneTracked(string imageId);
    }
}
