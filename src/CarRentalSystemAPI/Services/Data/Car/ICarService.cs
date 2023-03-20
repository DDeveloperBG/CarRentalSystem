namespace WebAPI.Services.Data.Car
{
    using WebAPI.DTOs.Car;

    public interface ICarService
    {
        Task AddCarRentingRequestAsync(AddCarRentingRequestInputDTO input);

        Task AddCarAdvertisementAsync(AddCarAdvertisementInputDTO input);
    }
}
