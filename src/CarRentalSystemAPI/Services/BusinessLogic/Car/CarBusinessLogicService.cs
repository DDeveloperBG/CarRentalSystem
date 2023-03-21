namespace WebAPI.Services.BusinessLogic.Car
{
    using WebAPI.Common;
    using WebAPI.DTOs.Car;
    using WebAPI.Models;
    using WebAPI.Services.BusinessLogic.CloudStorage;
    using WebAPI.Services.BusinessLogic.Image;
    using WebAPI.Services.Data.Car.Advertisement;
    using WebAPI.Services.Data.Car.RentingRequest;

    public class CarBusinessLogicService : ICarBusinessLogicService
    {
        private readonly ICarAdvertisementService carAdvertisementService;
        private readonly ICarRentingRequestService carRentingRequestService;
        private readonly IImageService imageService;
        private readonly ICloudStorageService cloudStorageService;

        public CarBusinessLogicService(
            ICarAdvertisementService carAdvertisementService,
            ICarRentingRequestService carRentingRequestService,
            IImageService imageService,
            ICloudStorageService cloudStorageService)
        {
            this.carAdvertisementService = carAdvertisementService;
            this.carRentingRequestService = carRentingRequestService;
            this.imageService = imageService;
            this.cloudStorageService = cloudStorageService;
        }

        public async Task<RequestResultDTO> AddCarRentingRequestAsync(AddCarRentingRequestInputDTO input)
        {
            try
            {
                await this.carRentingRequestService.AddNewAsync(input);
            }
            catch (Exception e)
            {
                return new RequestResultDTO
                {
                    IsSuccessful = false,
                    Message = e.Message,
                };
            }

            return new RequestResultDTO(true);
        }

        public async Task<RequestResultDTO> AddCarAdvertisementAsync(AddCarAdvertisementInputDTO input)
        {
            try
            {
                foreach (var imgFile in input.ImageFiles)
                {
                    var imgStream = await this.imageService
                        .ProcessAsync(imgFile.OpenReadStream(), 2);

                    var imgUrl = (await this.cloudStorageService
                        .SaveImageAsync(imgStream, GlobalConstants.CloudinaryCarImagesFolder)).Data;

                    input.ImageUrls.Add(imgUrl);
                }

                await this.carAdvertisementService.AddNewAsync(input);
            }
            catch (Exception e)
            {
                return new RequestResultDTO
                {
                    IsSuccessful = false,
                    Message = e.Message,
                };
            }

            return new RequestResultDTO(true);
        }
    }
}
