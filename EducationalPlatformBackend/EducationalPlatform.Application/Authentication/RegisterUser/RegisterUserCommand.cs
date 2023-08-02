using EducationalPlatform.Domain.Results;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    string RequestedRoleName,
    Guid? UserId) : IRequest<OneOf<NoContentResult, EmailInUseResult, NotAppropriateRoleResult>>;