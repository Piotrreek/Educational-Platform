using System.Text;
using EducationalPlatform.Domain.Exceptions;
using FluentValidation;

namespace EducationalPlatform.API.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EmailAlreadyConfirmedException emailAlreadyConfirmedException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(emailAlreadyConfirmedException.Message);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(validationException.Errors.Select(ve =>
                new { Field = ve.PropertyName, Message = ve.ErrorMessage }));
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(exception.Message);
        }
        
    }
}