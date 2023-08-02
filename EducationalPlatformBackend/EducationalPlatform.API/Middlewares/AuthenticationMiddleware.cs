namespace EducationalPlatform.API.Middlewares;

public class AuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authRequestHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        var jwtHeader = context.Request.Cookies[Keys.JwtHeader];
        var jwtSignature = context.Request.Cookies[Keys.JwtSignature];

        if (authRequestHeader is null || jwtHeader is null || jwtSignature is null ||
            !authRequestHeader.Contains("Bearer"))
        {
            await next(context);
        }
        else
        {
            var jwtPayload = authRequestHeader!.Split(" ")[1];
            var mergedJwt = string.Join('.', jwtHeader!, jwtPayload, jwtSignature!);
            context.Request.Headers["Authorization"] = $"Bearer {mergedJwt}";

            await next(context);
        }
    }
}