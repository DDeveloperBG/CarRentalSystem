namespace WebAPI.Services.Data.Car.RentingRequest
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.DTOs.Car.AddCarRentingRequest;
    using WebAPI.Services.Mapping;

    public class CarRentingRequestService : ICarRentingRequestService
    {
        private readonly IDeletableEntityRepository<CarRentingRequest> carRentingRequestRepository;

        public CarRentingRequestService(IDeletableEntityRepository<CarRentingRequest> carRentingRequestRepository)
        {
            this.carRentingRequestRepository = carRentingRequestRepository;
        }

        public IEnumerable<T> GetAllUnconfirmed<T>()
        {
            return this.carRentingRequestRepository
                .AllAsNoTracking()
                .Where(x => !x.IsConfirmed)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllOfUser<T>(bool forThePast, string userId, DateTime utcNow)
        {
            return this.carRentingRequestRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Where(x => !forThePast || (forThePast && x.ToDate < utcNow))
                .To<T>()
                .ToList();
        }

        public async Task AddNewAsync(AddCarRentingRequestInputDTO input)
        {
            var carRentRequest = AutoMapperConfig.MapperInstance.Map<CarRentingRequest>(input);

            await this.carRentingRequestRepository.AddAsync(carRentRequest);

            await this.carRentingRequestRepository.SaveChangesAsync();
        }

        public Task ConfirmOneAsync(string id)
        {
            var requestObj = this.GetOneTracked(id);

            requestObj.IsConfirmed = true;

            return this.carRentingRequestRepository.SaveChangesAsync();
        }

        private CarRentingRequest GetOneTracked(string id)
        {
            return this.carRentingRequestRepository
                .All()
                .Where(x => x.Id == id)
                .Single();
        }
    }
}
