namespace WebAPI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Common.Models;

    public class PickupLocation : BaseDeletableModel<string>
    {
        public PickupLocation()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarRentingRequests = new HashSet<CarRentingRequest>();
        }

        [Required]
        public string LocationName { get; set; }

        public ICollection<CarRentingRequest> CarRentingRequests { get; set; }
    }
}
