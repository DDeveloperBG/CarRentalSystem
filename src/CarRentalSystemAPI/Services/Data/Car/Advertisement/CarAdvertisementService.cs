namespace WebAPI.Services.Data.Car.Advertisement
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.DTOs.Car;
    using WebAPI.Services.Mapping;

    public class CarAdvertisementService : ICarAdvertisementService
    {
        private readonly IRepository<CarAdvertisement> carAdvertisementRepository;

        public CarAdvertisementService(IRepository<CarAdvertisement> carAdvertisementRepository)
        {
            this.carAdvertisementRepository = carAdvertisementRepository;
        }

        public async Task AddNewAsync(AddCarAdvertisementInputDTO input)
        {
            var carAd = AutoMapperConfig.MapperInstance.Map<CarAdvertisement>(input);

            await this.carAdvertisementRepository.AddAsync(carAd);

            await this.carAdvertisementRepository.SaveChangesAsync();
        }
    }
}
