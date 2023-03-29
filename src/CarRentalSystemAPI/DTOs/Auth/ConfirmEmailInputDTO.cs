namespace WebAPI.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class ConfirmEmailInputDTO : ValidatedInput
    {
        [Required]
        public string Code { get; set; }
    }
}
