namespace WebAPI.DTOs.Auth
{
    public class AuthenticationOutputDTO
    {
        public string JWT { get; set; }

        public string Username { get; set; }

        public string[] Roles { get; set; }
    }
}
