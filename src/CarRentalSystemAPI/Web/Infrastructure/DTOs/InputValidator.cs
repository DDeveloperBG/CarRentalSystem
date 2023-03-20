namespace WebAPI.Infrastructure.DTOs
{
    using System.ComponentModel.DataAnnotations;

    using WebAPI.DTOs;
    using WebAPI.Models;

    public static class InputValidator
    {
        public static RequestResultDTO ValidateInput(
            this ValidatedInput input)
        {
            var result = new RequestResultDTO()
            {
                IsSuccessful = false,
            };

            var validator = new ValidationContext(input);
            var allErrors = new List<ValidationResult>();

            if (!Validator.TryValidateObject(input, validator, allErrors, true))
            {
                result.Message = string.Join(Environment.NewLine, allErrors);

                return result;
            }

            try
            {
                input.Validate();
            }
            catch (Exception e)
            {
                result.Message = e.Message;

                return result;
            }

            result.IsSuccessful = true;
            return result;
        }
    }
}
