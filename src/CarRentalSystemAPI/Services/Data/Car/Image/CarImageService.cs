namespace WebAPI.Services.Data.Car.Image
{
    using WebAPI.Data.Common.Repositories;
    using WebAPI.Data.Models.Car;

    public class CarImageService : ICarImageService
    {
        private readonly IDeletableEntityRepository<CarImage> carImageRepository;

        public CarImageService(IDeletableEntityRepository<CarImage> carImageRepository)
        {
            this.carImageRepository = carImageRepository;
        }

        public Task DeleteOneAsync(string imgId)
        {
            var img = this.GetOneTracked(imgId);

            this.carImageRepository.Delete(img);

            return this.carImageRepository.SaveChangesAsync();
        }

        public async Task<string> AddNewAsync(string imgUrl)
        {
            var carImg = new CarImage
            {
                Url = imgUrl,
            };

            await this.carImageRepository.AddAsync(carImg);

            await this.carImageRepository.SaveChangesAsync();

            return carImg.Id;
        }

        public CarImage GetOneTracked(string imageId)
        {
            return this.carImageRepository
                .All()
                .Where(x => x.Id == imageId)
                .Single();
        }
    }
}
