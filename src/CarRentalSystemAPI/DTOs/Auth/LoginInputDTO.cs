namespace WebAPI.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Models;

    public class LoginInputDTO : ValidatedInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public override void Validate()
        {
            ApplicationUser.ValidateUsername(this.Username);
        }
    }
}
