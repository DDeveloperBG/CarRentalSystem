namespace WebAPI.Data.Models.Car
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Common.Models;

    public class CarAdvertisement : BaseDeletableModel<string>
    {
        public CarAdvertisement()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal RentPricePerDay { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public string CarCreationYear { get; set; }

        public int NumberPassengerSeats { get; set; }

        public string Description { get; set; }
    }
}
