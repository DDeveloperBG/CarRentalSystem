namespace WebAPI.Services.Data.Car.RentingRequest
{
    using WebAPI.DTOs.Car;

    public interface ICarRentingRequestService
    {
        Task AddNewAsync(AddCarRentingRequestInputDTO input);
    }
}
