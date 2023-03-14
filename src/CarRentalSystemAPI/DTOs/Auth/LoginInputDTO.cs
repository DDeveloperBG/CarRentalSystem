namespace WebAPI.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Models;

    public class LoginInputDTO : IValidatingInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public void Validate()
        {
            ApplicationUser.ValidateUsername(this.Username);
        }
    }
}
