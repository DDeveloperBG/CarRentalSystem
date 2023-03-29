namespace WebAPI.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using WebAPI.Common;
    using WebAPI.Data.Common.Models;

    public class CarRentingRequest : BaseDeletableModel<string>
    {
        public CarRentingRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool IsConfirmed { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [Required]
        [ForeignKey(nameof(PickupLocation))]
        public string PickupLocationId { get; set; }

        public PickupLocation PickupLocation { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }

        public CarAdvertisement Car { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public static void ValidateRentPeriod(DateTime from, DateTime to)
        {
            int minDays = ValidationConstants.Car.RentingRequest.MinPeriodInDays;

            if (to - from < TimeSpan.FromDays(minDays))
            {
                throw new ArgumentException(
                    $"Renting period should be at least {minDays} days!");
            }
        }
    }
}
