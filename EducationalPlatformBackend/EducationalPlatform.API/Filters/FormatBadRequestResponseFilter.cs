using EducationalPlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationalPlatform.API.Filters;

public class FormatBadRequestResponseFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();

        if (result.Result is BadRequestObjectResult objectResult)
        {
            result.Result =
                new BadRequestObjectResult(new ErrorMessage(objectResult.Value?.ToString() ?? string.Empty));
        }
    }
}