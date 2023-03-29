namespace WebAPI.DTOs.Car.GetCarAvertisementDetails
{
    using AutoMapper;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class GetCarAvertisementDetailsOutputDTO : IMapFrom<CarAdvertisement>, IHaveCustomMappings
    {
        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public decimal RentPricePerDay { get; set; }

        public string CarImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarAdvertisement, GetCarAvertisementDetailsOutputDTO>()
                .ForMember(
                    x => x.CarImageUrl,
                    opt => opt.MapFrom(src => src.CarImages.First().Url));
        }
    }
}
