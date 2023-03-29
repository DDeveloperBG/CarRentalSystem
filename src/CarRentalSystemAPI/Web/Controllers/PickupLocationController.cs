namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.DTOs.Car.GetAllPickUpLocations;
    using WebAPI.Models;
    using WebAPI.Services.Data.Car.Image;

    public class PickupLocationController : BaseApiController
    {
        private readonly IPickupLocationService pickupLocationService;

        public PickupLocationController(IPickupLocationService pickupLocationService)
        {
            this.pickupLocationService = pickupLocationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new RequestResultDTO<IEnumerable<OneOfGetAllPickUpLocations>>
            {
                IsSuccessful = true,
                Data = this.pickupLocationService.GetAll<OneOfGetAllPickUpLocations>(),
            };

            return this.Ok(response);
        }
    }
}
