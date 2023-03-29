namespace WebAPI.Services.Data.Car.Advertisement
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models;
    using WebAPI.Data.Models.Car;
    using WebAPI.DTOs.Car.AddCarAdvertisement;
    using WebAPI.Services.Mapping;

    public class CarAdvertisementService : ICarAdvertisementService
    {
        private readonly IDeletableEntityRepository<CarAdvertisement> carAdvertisementRepository;

        public CarAdvertisementService(
            IDeletableEntityRepository<CarAdvertisement> carAdvertisementRepository)
        {
            this.carAdvertisementRepository = carAdvertisementRepository;
        }

        public T GetOneUntracked<T>(string carAdvertisementId)
        {
            return this.carAdvertisementRepository
                .AllAsNoTracking()
                .Where(x => x.Id == carAdvertisementId)
                .To<T>()
                .Single();
        }

        public async Task DeleteOneAsync(
            string carAdvertisementId,
            Func<string, Task> deleteImg)
        {
            var result = this.carAdvertisementRepository
                .All()
                .Where(x => x.Id == carAdvertisementId)
                .Select(x => new
                {
                    carAd = x,
                    carImgIds = x.CarImages.Select(y => y.Id),
                })
                .Single();

            foreach (var imgId in result.carImgIds)
            {
                await deleteImg(imgId);
            }

            this.carAdvertisementRepository.Delete(result.carAd);

            await this.carAdvertisementRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllInRange<T>(DateTime from, DateTime to)
        {
            return this.carAdvertisementRepository
                .AllAsNoTracking()
                .Where(x => !x.CarRentingRequests
                    .Where(y => y.IsConfirmed)
                    .Any(y => y.FromDate <= to && y.ToDate >= from))
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.carAdvertisementRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public async Task AddNewAsync(
            AddCarAdvertisementInputDTO input,
            Func<string, CarImage> getImgById)
        {
            var carAd = AutoMapperConfig.MapperInstance.Map<CarAdvertisement>(input);

            foreach (var imgId in input.ImageIds)
            {
                carAd.CarImages.Add(getImgById(imgId));
            }

            await this.carAdvertisementRepository.AddAsync(carAd);

            await this.carAdvertisementRepository.SaveChangesAsync();
        }
    }
}
