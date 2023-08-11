using System.Security.Claims;
using EducationalPlatform.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace EducationPlatform.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid? UserId =>
        Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) ? userId : null;

    public string? RoleName => User?.FindFirst(ClaimTypes.Role)?.Value;

    public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;
}