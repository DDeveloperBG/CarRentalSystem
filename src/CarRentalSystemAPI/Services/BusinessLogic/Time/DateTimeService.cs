namespace WebAPI.Services.BusinessLogic.Time
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
