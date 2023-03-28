namespace WebAPI.DTOs.Car.GetAllUserRentingRequests
{
    using System.Text.Json.Serialization;

    using AutoMapper;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class OneGetAllUserRentingRequestsOutputDTO : IMapFrom<CarRentingRequest>, IHaveCustomMappings
    {
        public bool IsConfirmed { get; set; }

        public string FromDate { get; set; }

        [JsonIgnore]
        public DateTime FromDateAsDateTime { get; set; }

        public string ToDate { get; set; }

        [JsonIgnore]
        public DateTime ToDateAsDateTime { get; set; }

        public string PickupLocation { get; set; }

        public string Car { get; set; }

        public decimal Price => ((this.ToDateAsDateTime - this.FromDateAsDateTime).Days + 1) * this.RentPricePerDay;

        [JsonIgnore]
        public decimal RentPricePerDay { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarRentingRequest, OneGetAllUserRentingRequestsOutputDTO>()
                .ForMember(
                    x => x.FromDate,
                    opt => opt.MapFrom(src => src.FromDate.ToString("M/d/yyyy")))
                .ForMember(
                    x => x.ToDate,
                    opt => opt.MapFrom(src => src.ToDate.ToString("M/d/yyyy")))
                .ForMember(
                    x => x.FromDateAsDateTime,
                    opt => opt.MapFrom(src => src.FromDate))
                .ForMember(
                    x => x.ToDateAsDateTime,
                    opt => opt.MapFrom(src => src.ToDate))
                .ForMember(
                    x => x.PickupLocation,
                    opt => opt.MapFrom(src => src.PickupLocation.LocationName))
                .ForMember(
                    x => x.Car,
                    opt => opt.MapFrom(src => $"{src.Car.CarBrand} {src.Car.CarModel}"))
                .ForMember(
                    x => x.RentPricePerDay,
                    opt => opt.MapFrom(src => src.Car.RentPricePerDay));
        }
    }
}
