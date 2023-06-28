using EducationalPlatform.Application.Builders;
using EducationalPlatform.Domain.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class UserRegisteredHandler : AccountConfirmationLinkSender, INotificationHandler<UserRegistered>
{
    public UserRegisteredHandler(IEmailService emailService, IConfiguration configuration,
        ILogger<UserRegisteredHandler> logger, EmailMessageBuilder emailMessageBuilder) : base(emailService,
        configuration, logger, emailMessageBuilder)
    {
    }

    public Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        SendConfirmationLink(notification.UserId.ToString(), notification.Token, notification.Email);

        return Task.CompletedTask;
    }
}