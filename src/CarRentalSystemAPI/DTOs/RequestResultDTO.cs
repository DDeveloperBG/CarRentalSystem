namespace WebAPI.Models
{
    public class RequestResultDTO<T>
    {
        public T Data { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public Enum DangerLevel { get; set; }
    }
}
