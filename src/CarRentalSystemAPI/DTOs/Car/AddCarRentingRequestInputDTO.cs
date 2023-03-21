namespace WebAPI.DTOs.Car
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class AddCarRentingRequestInputDTO : ValidatedInput, IMapTo<CarRentingRequest>
    {
        public IFormFile[] CarImages { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [Required]
        public string PickupLocationId { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public string UserId { get; set; }

        public override void Validate()
        {
            CarRentingRequest.ValidateRentPeriod(this.FromDate, this.ToDate);
        }
    }
}
