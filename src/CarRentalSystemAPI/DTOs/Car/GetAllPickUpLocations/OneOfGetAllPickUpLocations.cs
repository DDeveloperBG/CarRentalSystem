namespace WebAPI.DTOs.Car.GetAllPickUpLocations
{
    using WebAPI.Data.Models;
    using WebAPI.Services.Mapping;

    public class OneOfGetAllPickUpLocations : IMapFrom<PickupLocation>
    {
        public string Id { get; set; }

        public string LocationName { get; set; }
    }
}
