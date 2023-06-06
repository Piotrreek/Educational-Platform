using EducationalPlatform.Domain.Primitives;
using MediatR;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber,
    string RoleName,
    Guid? UserId) : IRequest<Result>;