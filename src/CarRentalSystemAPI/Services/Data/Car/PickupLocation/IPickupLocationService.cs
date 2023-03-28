namespace WebAPI.Services.Data.Car.Image
{
    using WebAPI.Data.Models;

    public interface IPickupLocationService
    {
        IEnumerable<T> GetAll<T>();

        PickupLocation GetOneTracked(string pickupLocationId);
    }
}
