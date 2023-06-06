using EducationalPlatform.Domain.Primitives;
using MediatR;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber,
    string RequestedRoleName,
    Guid? UserId) : IRequest<Result>;