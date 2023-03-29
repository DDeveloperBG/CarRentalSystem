namespace WebAPI.Services.Data.Car.Advertisement
{
    using WebAPI.Data.Models.Car;
    using WebAPI.DTOs.Car.AddCarAdvertisement;

    public interface ICarAdvertisementService
    {
        T GetOneUntracked<T>(string carAdvertisementId);

        Task DeleteOneAsync(string carAdvertisementId, Func<string, Task> deleteImg);

        IEnumerable<T> GetAllInRange<T>(DateTime from, DateTime to);

        IEnumerable<T> GetAll<T>();

        Task AddNewAsync(AddCarAdvertisementInputDTO input, Func<string, CarImage> getImgById);
    }
}
