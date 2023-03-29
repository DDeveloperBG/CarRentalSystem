namespace WebAPI.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.Data.Models;

    public class RegisterInputDTO : ValidatedInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PersonalIdentificationNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public override void Validate()
        {
            ApplicationUser.ValidateUsername(this.Username);

            ApplicationUser.ValidateSurname(this.Surname);

            ApplicationUser.ValidateForename(this.Forename);

            ApplicationUser.ValidatePhoneNumber(this.PhoneNumber);

            ApplicationUser.ValidatePersonalIdentificationNumber(this.PersonalIdentificationNumber);

            ApplicationUser.ValidateEmail(this.Email);
        }
    }
}
