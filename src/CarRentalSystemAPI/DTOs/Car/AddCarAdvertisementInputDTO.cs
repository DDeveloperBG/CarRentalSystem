﻿namespace WebAPI.DTOs.Car
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using WebAPI.Common;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class AddCarAdvertisementInputDTO : ValidatedInput, IMapTo<CarAdvertisement>
    {
        public AddCarAdvertisementInputDTO()
        {
            this.ImageUrls = new List<string>();
        }

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

        public IList<IFormFile> ImageFiles { get; set; }

        public IList<string> ImageUrls { get; set; }

        public override void Validate()
        {
            int minCountImgs = ValidationConstants.Car.Advertisement.MinCountOfPictures;
            if (this.ImageFiles.Count < minCountImgs)
            {
                throw new ArgumentException($"Car images should be at least {minCountImgs}!");
            }
        }
    }
}
