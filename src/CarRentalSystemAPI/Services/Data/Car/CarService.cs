namespace WebAPI.Services.Data.Car
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.DTOs.Car;
    using WebAPI.Services.Mapping;

    public class CarService : ICarService
    {
        private readonly IRepository<CarAdvertisement> carAdvertisementRepository;
        private readonly IRepository<CarRentingRequest> carRentingRequestRepository;
        private readonly IRepository<PickupLocation> pickupLocationRepository;

        public CarService(
            IRepository<CarAdvertisement> carAdvertisementRepository,
            IRepository<CarRentingRequest> carRentingRequestRepository,
            IRepository<PickupLocation> pickupLocationRepository)
        {
            this.carAdvertisementRepository = carAdvertisementRepository;
            this.carRentingRequestRepository = carRentingRequestRepository;
            this.pickupLocationRepository = pickupLocationRepository;
        }

        public async Task AddCarRentingRequestAsync(AddCarRentingRequestInputDTO input)
        {
            var carRentRequest = AutoMapperConfig.MapperInstance.Map<CarRentingRequest>(input);

            await this.carRentingRequestRepository.AddAsync(carRentRequest);

            await this.carRentingRequestRepository.SaveChangesAsync();
        }

        public async Task AddCarAdvertisementAsync(AddCarAdvertisementInputDTO input)
        {
            var carAd = AutoMapperConfig.MapperInstance.Map<CarAdvertisement>(input);

            await this.carAdvertisementRepository.AddAsync(carAd);

            await this.carAdvertisementRepository.SaveChangesAsync();
        }
    }
}
