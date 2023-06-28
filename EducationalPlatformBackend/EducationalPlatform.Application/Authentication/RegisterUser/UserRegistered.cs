using MediatR;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public record UserRegistered(Guid UserId, string Email, string Token) : INotification;