using System.Security.Claims;

namespace EducationalPlatform.Application.Abstractions.Services;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    Guid? UserId { get; }
    string? RoleName { get; }
    string? Email { get; }
}