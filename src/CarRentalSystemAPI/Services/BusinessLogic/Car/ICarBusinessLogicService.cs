namespace WebAPI.Services.BusinessLogic.Car
{
    using WebAPI.DTOs.Car.AddCarAdvertisement;
    using WebAPI.DTOs.Car.AddCarRentingRequest;
    using WebAPI.DTOs.Car.GetAllCarAdvertisements;
    using WebAPI.DTOs.Car.GetAllUnconfirmedRentRequests;
    using WebAPI.DTOs.Car.GetAllUserRentingRequests;
    using WebAPI.DTOs.Car.GetCarAvertisementDetails;

    using WebAPI.Models;

    public interface ICarBusinessLogicService
    {
        Task<RequestResultDTO> ConfirmRentingRequestAsync(string requestId);

        RequestResultDTO<IEnumerable<OneOfGetAllUnconfirmedRentRequestsOutputDTO>> GetUnconfirmedRentRequestsAsync();

        RequestResultDTO<GetCarAvertisementDetailsOutputDTO> GetAdvertisementDetails(string advertisementId);

        Task<RequestResultDTO> DeleteAdvertisementAsync(string advertisementId);

        RequestResultDTO<IEnumerable<OneGetAllUserRentingRequestsOutputDTO>> GetAllUserRentingRequests(bool forThePast, string userId);

        RequestResultDTO<GetAllCarAdvertisementsOutputDTO> GetAllCarAdvertisements(GetAllCarAdvertisementsInputDTO input);

        Task<RequestResultDTO> AddCarRentingRequestAsync(AddCarRentingRequestInputDTO input);

        Task<RequestResultDTO> AddCarAdvertisementAsync(AddCarAdvertisementInputDTO input);
    }
}
