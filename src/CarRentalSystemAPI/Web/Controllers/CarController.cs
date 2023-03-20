namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WebAPI.Common;
    using WebAPI.DTOs.Car;
    using WebAPI.Services.Data.Car;

    public class CarController : BaseApiController
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> AddCarRentingRequest(AddCarRentingRequestInputDTO input)
        {
            input.UserId = this.GetUserId();

            await this.carService.AddCarRentingRequestAsync(input);

            return this.Ok();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.AdministratorRoleName)]
        public async Task<IActionResult> AddCarAdvertisement(AddCarAdvertisementInputDTO input)
        {
            await this.carService.AddCarAdvertisementAsync(input);

            return this.Ok();
        }
    }
}
