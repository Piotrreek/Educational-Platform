using EducationalPlatform.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationalPlatform.API.Filters;

public class DoNotAllowUserWithUserRole : IAsyncActionFilter
{
    private readonly IUserContextService _userContextService;

    public DoNotAllowUserWithUserRole(IUserContextService userContextService)
    {
        _userContextService = userContextService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_userContextService.RoleName == "User")
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.HttpContext.Response.WriteAsync("You are not authorized to register new account");
        }
        
        await next();
    }
}