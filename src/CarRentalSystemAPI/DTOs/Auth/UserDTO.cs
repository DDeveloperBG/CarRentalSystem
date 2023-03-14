namespace WebAPI.DTOs.Auth
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string[] Roles { get; set; }
    }
}
