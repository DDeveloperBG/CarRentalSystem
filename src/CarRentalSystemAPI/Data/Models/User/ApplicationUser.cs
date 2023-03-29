namespace WebAPI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Microsoft.AspNetCore.Identity;
    using WebAPI.Common;
    using WebAPI.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.CarRentingRequests = new HashSet<CarRentingRequest>();
        }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string PersonalIdentificationNumber { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public ICollection<CarRentingRequest> CarRentingRequests { get; set; }

        public static void ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)
                    || username.Length < ValidationConstants.User.MinUsernameLength
                    || username.Length > ValidationConstants.User.MaxUsernameLength)
            {
                throw new ArgumentException($"{nameof(UserName)} should be between {ValidationConstants.User.MinUsernameLength} and {ValidationConstants.User.MaxUsernameLength} symbols!");
            }
        }

        public static void ValidateForename(string forename)
        {
            if (string.IsNullOrEmpty(forename)
               || forename.Length < ValidationConstants.User.MinForenameLength
               || forename.Length > ValidationConstants.User.MaxForenameLength)
            {
                throw new ArgumentException($"{nameof(Forename)} should be between {ValidationConstants.User.MinForenameLength} and {ValidationConstants.User.MaxForenameLength} symbols!");
            }

            if (!forename.All(char.IsLetter))
            {
                throw new ArgumentException($"{nameof(Forename)} should contain only letters!");
            }
        }

        public static void ValidateSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname)
               || surname.Length < ValidationConstants.User.MinSurnameLength
               || surname.Length > ValidationConstants.User.MaxSurnameLength)
            {
                throw new ArgumentException($"{nameof(Surname)} should be between {ValidationConstants.User.MinSurnameLength} and {ValidationConstants.User.MaxSurnameLength} symbols!");
            }

            if (!surname.All(char.IsLetter))
            {
                throw new ArgumentException($"{nameof(Surname)} should contain only letters!");
            }
        }

        public static void ValidatePhoneNumber(string phoneNumber)
        {
            var rgx = new Regex(ValidationConstants.User.PhoneNumberRegex);

            if (!rgx.IsMatch(phoneNumber))
            {
                throw new ArgumentException($"{nameof(PhoneNumber)} is in invalid format!");
            }
        }

        public static void ValidatePersonalIdentificationNumber(string pin)
        {
            if (!pin.All(char.IsDigit))
            {
                throw new ArgumentException($"{nameof(PersonalIdentificationNumber)} should contain only digits!");
            }

            if (pin.Length != ValidationConstants.User.PINLength)
            {
                throw new ArgumentException($"{nameof(PersonalIdentificationNumber)} length should be {ValidationConstants.User.PINLength}!");
            }

            if (!BulgarianPIN.IsValid(pin))
            {
                throw new ArgumentException($"{nameof(PersonalIdentificationNumber)} is invalid!");
            }
        }

        public static void ValidateEmail(string email)
        {
            var rgx = new Regex(ValidationConstants.User.EmailRegex);

            if (!rgx.IsMatch(email))
            {
                throw new ArgumentException($"{nameof(Email)} is in invalid format!");
            }
        }
    }
}
