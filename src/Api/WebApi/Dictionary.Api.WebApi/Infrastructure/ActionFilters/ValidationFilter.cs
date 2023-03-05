using Microsoft.AspNetCore.Mvc.Filters;

namespace Dictionary.Api.WebApi
{
    public class ValidateModelStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var messages = context.ModelState.Values.SelectMany(s => s.Errors)
                                                        .Select(s => !string.IsNullOrEmpty(s.ErrorMessage) ? s.ErrorMessage : s.Exception?.Message)
                                                        .Distinct().ToList();

                return;
            }

            await next();
        }
    }
}
