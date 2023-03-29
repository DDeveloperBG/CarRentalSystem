namespace WebAPI.DTOs.Car.GetAllCarAdvertisements
{
    using WebAPI.Data.Models;

    public class GetAllCarAdvertisementsInputDTO : ValidatedInput
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public override void Validate()
        {
            if (this.FromDate != null && this.ToDate != null)
            {
                CarRentingRequest.ValidateRentPeriod(this.FromDate.Value, this.ToDate.Value);
            }

            if (this.FromDate == null && this.ToDate == null)
            {
                return;
            }

            throw new ArgumentException("One of the required parameters is missing!");
        }
    }
}
