namespace WebAPI.Models
{
    public class RequestResultDTO<T> : RequestResultDTO
    {
        public T Data { get; set; }
    }

    public class RequestResultDTO
    {
        public RequestResultDTO()
        {
        }

        public RequestResultDTO(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public Enum DangerLevel { get; set; }
    }
}
