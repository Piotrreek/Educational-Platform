using MediatR;

namespace EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;

public record AccountConfirmationTokenAddedToUser(Guid UserId, string Email, string Token) : INotification;