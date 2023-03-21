namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WebAPI.Common;
    using WebAPI.DTOs.Car;
    using WebAPI.Services.BusinessLogic.Car;

    public class CarController : BaseApiController
    {
        private readonly ICarBusinessLogicService carService;

        public CarController(ICarBusinessLogicService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> AddRentingRequest(AddCarRentingRequestInputDTO input)
        {
            input.UserId = this.GetUserId();

            var response = await this.carService.AddCarRentingRequestAsync(input);

            return this.Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public async Task<IActionResult> AddAdvertisement(AddCarAdvertisementInputDTO input)
        {
            var response = await this.carService.AddCarAdvertisementAsync(input);

            return this.Ok(response);
        }
    }
}
