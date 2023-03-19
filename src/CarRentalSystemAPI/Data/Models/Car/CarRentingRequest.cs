namespace WebAPI.Data.Models.Car
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using WebAPI.Data.Common.Models;

    public class CarRentingRequest : BaseDeletableModel<string>
    {
        public CarRentingRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string PickupLocation { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }

        public CarAdvertisement Car { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
