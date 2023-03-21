namespace WebAPI.Services.BusinessLogic.Car
{
    using WebAPI.DTOs.Car;
    using WebAPI.Models;

    public interface ICarBusinessLogicService
    {
        Task<RequestResultDTO> AddCarRentingRequestAsync(AddCarRentingRequestInputDTO input);

        Task<RequestResultDTO> AddCarAdvertisementAsync(AddCarAdvertisementInputDTO input);
    }
}
