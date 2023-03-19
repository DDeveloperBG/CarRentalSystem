namespace WebAPI.Data.Models.Car
{
    using System;

    using WebAPI.Data.Common.Models;

    public class CarAdvertisement : BaseDeletableModel<string>
    {
        public CarAdvertisement()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal RentPricePerDay { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string CarCreationYear { get; set; }

        public int NumberPassengerSeats { get; set; }

        public string Description { get; set; }
    }
}
