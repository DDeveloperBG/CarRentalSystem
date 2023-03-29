namespace WebAPI.Services.BusinessLogic.Car
{
    using System.Collections.Generic;

    using WebAPI.Common;

    using WebAPI.DTOs.Car.AddCarAdvertisement;
    using WebAPI.DTOs.Car.AddCarRentingRequest;
    using WebAPI.DTOs.Car.GetAllCarAdvertisements;
    using WebAPI.DTOs.Car.GetAllUnconfirmedRentRequests;
    using WebAPI.DTOs.Car.GetAllUserRentingRequests;
    using WebAPI.DTOs.Car.GetCarAvertisementDetails;

    using WebAPI.Models;

    using WebAPI.Services.BusinessLogic.CloudStorage;
    using WebAPI.Services.BusinessLogic.Image;
    using WebAPI.Services.BusinessLogic.Time;
    using WebAPI.Services.Data.Car.Advertisement;
    using WebAPI.Services.Data.Car.Image;
    using WebAPI.Services.Data.Car.RentingRequest;

    public class CarBusinessLogicService : ICarBusinessLogicService
    {
        private readonly ICarAdvertisementService carAdvertisementService;
        private readonly ICarRentingRequestService carRentingRequestService;
        private readonly ICarImageService carImageService;
        private readonly IImageService imageService;
        private readonly ICloudStorageService cloudStorageService;
        private readonly IDateTimeService dateTimeService;

        public CarBusinessLogicService(
            ICarAdvertisementService carAdvertisementService,
            ICarRentingRequestService carRentingRequestService,
            ICarImageService carImageService,
            IImageService imageService,
            ICloudStorageService cloudStorageService,
            IDateTimeService dateTimeService)
        {
            this.carAdvertisementService = carAdvertisementService;
            this.carRentingRequestService = carRentingRequestService;
            this.carImageService = carImageService;
            this.imageService = imageService;
            this.cloudStorageService = cloudStorageService;
            this.dateTimeService = dateTimeService;
        }

        public async Task<RequestResultDTO> ConfirmRentingRequestAsync(string requestId)
        {
            try
            {
                await this.carRentingRequestService.ConfirmOneAsync(requestId);

                return new RequestResultDTO(true);
            }
            catch (Exception ex)
            {
                return new RequestResultDTO
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public RequestResultDTO<IEnumerable<OneOfGetAllUnconfirmedRentRequestsOutputDTO>> GetUnconfirmedRentRequestsAsync()
        {
            try
            {
                return new RequestResultDTO<IEnumerable<OneOfGetAllUnconfirmedRentRequestsOutputDTO>>
                {
                    IsSuccessful = true,
                    Data = this.carRentingRequestService
                        .GetAllUnconfirmed<OneOfGetAllUnconfirmedRentRequestsOutputDTO>(),
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDTO<IEnumerable<OneOfGetAllUnconfirmedRentRequestsOutputDTO>>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public RequestResultDTO<GetCarAvertisementDetailsOutputDTO> GetAdvertisementDetails(string advertisementId)
        {
            try
            {
                return new RequestResultDTO<GetCarAvertisementDetailsOutputDTO>
                {
                    IsSuccessful = true,
                    Data = this.carAdvertisementService
                        .GetOneUntracked<GetCarAvertisementDetailsOutputDTO>(advertisementId),
                };
            }
            catch (Exception ex)
            {
                return new RequestResultDTO<GetCarAvertisementDetailsOutputDTO>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResultDTO> DeleteAdvertisementAsync(string advertisementId)
        {
            try
            {
                await this.carAdvertisementService.DeleteOneAsync(
                    advertisementId,
                    this.carImageService.DeleteOneAsync);

                return new RequestResultDTO(true);
            }
            catch (Exception ex)
            {
                return new RequestResultDTO
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public RequestResultDTO<IEnumerable<OneGetAllUserRentingRequestsOutputDTO>> GetAllUserRentingRequests(bool forThePast, string userId)
        {
            try
            {
                return new RequestResultDTO<IEnumerable<OneGetAllUserRentingRequestsOutputDTO>>
                {
                    IsSuccessful = true,
                    Data = this.carRentingRequestService
                        .GetAllOfUser<OneGetAllUserRentingRequestsOutputDTO>(forThePast, userId, this.dateTimeService.GetUtcNow()),
                };
            }
            catch (Exception e)
            {
                return new RequestResultDTO<IEnumerable<OneGetAllUserRentingRequestsOutputDTO>>
                {
                    IsSuccessful = false,
                    Message = e.Message,
                };
            }
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
                input.ImageIds.Clear();

                var imgUrls = new List<string>();
                foreach (var imgFile in input.ImageFiles)
                {
                    var imgStream = await this.imageService
                        .ProcessAsync(imgFile.OpenReadStream(), GlobalConstants.CarImgWidth);

                    var imgUrl = (await this.cloudStorageService
                        .SaveImageAsync(imgStream, GlobalConstants.CloudinaryCarImagesFolder)).Data;

                    var imgId = await this.carImageService.AddNewAsync(imgUrl);

                    input.ImageIds.Add(imgId);
                }

                await this.carAdvertisementService.AddNewAsync(input, this.carImageService.GetOneTracked);
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

        public RequestResultDTO<GetAllCarAdvertisementsOutputDTO> GetAllCarAdvertisements(GetAllCarAdvertisementsInputDTO input)
        {
            var all = input.FromDate != null ?
                this.carAdvertisementService.GetAllInRange<OneOfGetAllCarAdvertisementsOutputDTO>(input.FromDate.Value, input.ToDate.Value) :
                this.carAdvertisementService.GetAll<OneOfGetAllCarAdvertisementsOutputDTO>();

            var data = new GetAllCarAdvertisementsOutputDTO
            {
                All = all,
            };

            return new RequestResultDTO<GetAllCarAdvertisementsOutputDTO>
            {
                IsSuccessful = true,
                Data = data,
            };
        }
    }
}
