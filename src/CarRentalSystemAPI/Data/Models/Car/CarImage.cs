namespace WebAPI.Data.Models.Car
{
    using System;

    using WebAPI.Data.Common.Models;

    public class CarImage : BaseDeletableModel<string>
    {
        public CarImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }
    }
}
