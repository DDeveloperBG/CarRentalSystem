namespace WebAPI.Infrastructure.Filter
{
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using WebAPI.DTOs;
    using WebAPI.Infrastructure.DTOs;

    public class InputValidatingFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var inputType = context.ActionDescriptor.Parameters.FirstOrDefault()?.ParameterType;

            // TODO: Make it work with multipart/form-data!
            if (
                context.HttpContext.Request.ContentType != null &&
                !context.HttpContext.Request.ContentType.Contains("multipart/form-data"))
            {
                if (inputType.IsSubclassOf(typeof(ValidatedInput)))
                {
                    var requestBody = await ReadRequestBodyAsync(context.HttpContext.Request);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var inputData = JsonSerializer
                        .Deserialize(requestBody, inputType, options) as ValidatedInput;

                    var validationResult = inputData.ValidateInput();

                    if (!validationResult.IsSuccessful)
                    {
                        context.Result = new OkObjectResult(validationResult);
                        return;
                    }
                }
            }

            await next();
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Position = 0;

            using var streamReader = new StreamReader(request.Body);
            string requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;

            return requestBody;
        }
    }
}
