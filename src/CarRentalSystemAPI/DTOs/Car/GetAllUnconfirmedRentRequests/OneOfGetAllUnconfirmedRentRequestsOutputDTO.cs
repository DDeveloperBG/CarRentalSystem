namespace WebAPI.DTOs.Car.GetAllUnconfirmedRentRequests
{
    using System;
    using System.Text.Json.Serialization;

    using AutoMapper;
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class OneOfGetAllUnconfirmedRentRequestsOutputDTO : IMapFrom<CarRentingRequest>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string RentingPeriod => $"{this.FromDate.ToString("M/d/yyyy")} - {this.ToDate.ToString("M/d/yyyy")}";

        [JsonIgnore]
        public DateTime FromDate { get; set; }

        [JsonIgnore]
        public DateTime ToDate { get; set; }

        public string PickupLocation { get; set; }

        public string Car { get; set; }

        public decimal Price => ((this.ToDate - this.FromDate).Days + 1) * this.RentPricePerDay;

        [JsonIgnore]
        public decimal RentPricePerDay { get; set; }

        public string UserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserPIN { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarRentingRequest, OneOfGetAllUnconfirmedRentRequestsOutputDTO>()
                .ForMember(
                    x => x.PickupLocation,
                    opt => opt.MapFrom(src => src.PickupLocation.LocationName))
                .ForMember(
                    x => x.Car,
                    opt => opt.MapFrom(src => $"{src.Car.CarBrand} {src.Car.CarModel}"))
                .ForMember(
                    x => x.RentPricePerDay,
                    opt => opt.MapFrom(src => src.Car.RentPricePerDay))
                 .ForMember(
                    x => x.UserName,
                    opt => opt.MapFrom(src => src.User.Forename + " " + src.User.Surname))
                 .ForMember(
                    x => x.UserPIN,
                    opt => opt.MapFrom(src => src.User.PersonalIdentificationNumber));
        }
    }
}
