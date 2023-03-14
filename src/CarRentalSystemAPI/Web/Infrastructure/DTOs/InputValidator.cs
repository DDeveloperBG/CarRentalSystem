namespace WebAPI.Infrastructure.DTOs
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using WebAPI.DTOs;
    using WebAPI.Models;

    public static class InputValidator
    {
        public static RequestResultDTO<object> ValidateInput(
            this IValidatingInput input,
            ModelStateDictionary modelState)
        {
            var result = new RequestResultDTO<object>()
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
