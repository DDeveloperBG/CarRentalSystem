namespace WebAPI.DTOs.Car
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class AddCarRentingRequestInputDTO : ValidatedInput, IMapTo<CarRentingRequest>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [Required]
        public string PickupLocationId { get; set; }

        [Required]
        public string CarId { get; set; }

        public string UserId { get; set; }

        public override void Validate()
        {
            CarRentingRequest.ValidateRentPeriod(this.FromDate, this.ToDate);
        }
    }
}
