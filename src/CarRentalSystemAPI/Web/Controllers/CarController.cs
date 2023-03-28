namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WebAPI.Common;
    using WebAPI.DTOs.Car.AddCarAdvertisement;
    using WebAPI.DTOs.Car.AddCarRentingRequest;
    using WebAPI.DTOs.Car.GetAllCarAdvertisements;
    using WebAPI.Services.BusinessLogic.Car;

    public class CarController : BaseApiController
    {
        private readonly ICarBusinessLogicService carService;

        public CarController(ICarBusinessLogicService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult GetAdvertisementDetails([FromQuery] string advertisementId)
        {
            var response = this.carService.GetAdvertisementDetails(advertisementId);

            return this.Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllAdvertisements([FromQuery] GetAllCarAdvertisementsInputDTO input)
        {
            var response = this.carService.GetAllCarAdvertisements(input);

            return this.Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public IActionResult GetAllUserRentingRequests([FromQuery] bool forThePast)
        {
            string userId = this.GetUserId();

            var response = this.carService.GetAllUserRentingRequests(forThePast, userId);

            return this.Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> AddRentingRequest([FromBody] AddCarRentingRequestInputDTO input)
        {
            input.UserId = this.GetUserId();

            var response = await this.carService.AddCarRentingRequestAsync(input);

            return this.Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public async Task<IActionResult> ConfirmRentingRequest([FromForm] string requestId)
        {
            var response = await this.carService.ConfirmRentingRequestAsync(requestId);

            return this.Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public IActionResult GetAllUnconfirmedRentingRequests()
        {
            var response = this.carService.GetUnconfirmedRentRequestsAsync();

            return this.Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public async Task<IActionResult> AddAdvertisement([FromForm] AddCarAdvertisementInputDTO input)
        {
            var response = await this.carService.AddCarAdvertisementAsync(input);

            return this.Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAdvertisement([FromForm] string advertisementId)
        {
            var response = await this.carService.DeleteAdvertisementAsync(advertisementId);

            return this.Ok(response);
        }
    }
}
