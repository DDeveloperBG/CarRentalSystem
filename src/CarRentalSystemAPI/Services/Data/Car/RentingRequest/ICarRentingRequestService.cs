namespace WebAPI.Services.Data.Car.RentingRequest
{
    using WebAPI.DTOs.Car.AddCarRentingRequest;

    public interface ICarRentingRequestService
    {
        Task ConfirmOneAsync(string id);

        IEnumerable<T> GetAllUnconfirmed<T>();

        IEnumerable<T> GetAllOfUser<T>(bool forThePast, string userId, DateTime utcNow);

        Task AddNewAsync(AddCarRentingRequestInputDTO input);
    }
}
