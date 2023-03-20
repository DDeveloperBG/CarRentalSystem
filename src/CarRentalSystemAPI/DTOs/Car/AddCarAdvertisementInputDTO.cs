namespace WebAPI.DTOs.Car
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class AddCarAdvertisementInputDTO : ValidatedInput, IMapTo<CarAdvertisement>
    {
        [Required]
        public decimal RentPricePerDay { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public string CarCreationYear { get; set; }

        [Required]
        public int NumberPassengerSeats { get; set; }

        public string Description { get; set; }
    }
}
