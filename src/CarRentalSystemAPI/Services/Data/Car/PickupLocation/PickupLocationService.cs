namespace WebAPI.Services.Data.Car.Image
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class PickupLocationService : IPickupLocationService
    {
        private readonly IDeletableEntityRepository<PickupLocation> pickupLocationRepository;

        public PickupLocationService(IDeletableEntityRepository<PickupLocation> pickupLocationRepository)
        {
            this.pickupLocationRepository = pickupLocationRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.pickupLocationRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public PickupLocation GetOneTracked(string pickupLocationId)
        {
            return this.pickupLocationRepository
                .All()
                .Where(x => x.Id == pickupLocationId)
                .Single();
        }
    }
}
