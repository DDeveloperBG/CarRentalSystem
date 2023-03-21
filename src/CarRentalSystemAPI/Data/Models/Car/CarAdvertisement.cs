namespace WebAPI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Common.Models;
    using WebAPI.Data.Models.Car;

    public class CarAdvertisement : BaseDeletableModel<string>
    {
        public CarAdvertisement()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarRentingRequests = new HashSet<CarRentingRequest>();
            this.CarImages = new HashSet<CarImage>();
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

        public ICollection<CarImage> CarImages { get; set; }

        public ICollection<CarRentingRequest> CarRentingRequests { get; set; }
    }
}
