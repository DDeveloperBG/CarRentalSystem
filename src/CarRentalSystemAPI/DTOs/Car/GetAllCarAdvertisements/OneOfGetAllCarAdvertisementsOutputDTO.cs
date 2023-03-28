namespace WebAPI.DTOs.Car.GetAllCarAdvertisements
{
    using AutoMapper;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class OneOfGetAllCarAdvertisementsOutputDTO : IMapFrom<CarAdvertisement>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public decimal RentPricePerDay { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string TransmissionType { get; set; }

        public int NumberPassengerSeats { get; set; }

        public string CarCreationYear { get; set; }

        public string CarImageUrl { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarAdvertisement, OneOfGetAllCarAdvertisementsOutputDTO>()
                .ForMember(
                    x => x.CarImageUrl,
                    opt => opt.MapFrom(src => src.CarImages.First().Url));
        }
    }
}
