namespace WebAPI.Services.Data.Car.RentingRequest
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.DTOs.Car;
    using WebAPI.Services.Mapping;

    public class CarRentingRequestService : ICarRentingRequestService
    {
        private readonly IRepository<CarRentingRequest> carRentingRequestRepository;

        public CarRentingRequestService(IRepository<CarRentingRequest> carRentingRequestRepository)
        {
            this.carRentingRequestRepository = carRentingRequestRepository;
        }

        public async Task AddNewAsync(AddCarRentingRequestInputDTO input)
        {
            var carRentRequest = AutoMapperConfig.MapperInstance.Map<CarRentingRequest>(input);

            await this.carRentingRequestRepository.AddAsync(carRentRequest);

            await this.carRentingRequestRepository.SaveChangesAsync();
        }
    }
}
