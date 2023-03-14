namespace WebAPI.DTOs.Auth
{
    public class JWTSettings
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }
    }
}
