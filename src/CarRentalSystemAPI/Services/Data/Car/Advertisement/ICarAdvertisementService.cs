namespace WebAPI.Services.Data.Car.Advertisement
{
    using WebAPI.DTOs.Car;

    public interface ICarAdvertisementService
    {
        Task AddNewAsync(AddCarAdvertisementInputDTO input);
    }
}
