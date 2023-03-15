namespace WebAPI.Infrastructure.DTOs
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using WebAPI.DTOs;
    using WebAPI.Models;

    public static class InputValidator
    {
        public static RequestResultDTO ValidateInput(
            this ValidatedInput input,
            ModelStateDictionary modelState)
        {
            var result = new RequestResultDTO()
            {
                IsSuccessful = false,
            };

            if (!modelState.IsValid)
            {
                var allErrors = modelState.Values.SelectMany(v => v.Errors);
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
