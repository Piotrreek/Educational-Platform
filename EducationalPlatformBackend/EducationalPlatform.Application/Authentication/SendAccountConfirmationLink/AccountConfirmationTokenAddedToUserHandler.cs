using EducationalPlatform.Application.Builders;
using EducationalPlatform.Domain.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;

public class AccountConfirmationTokenAddedToUserHandler : AccountConfirmationLinkSender,
    INotificationHandler<AccountConfirmationTokenAddedToUser>
{
    public AccountConfirmationTokenAddedToUserHandler(IEmailService emailService, IConfiguration configuration,
        ILogger<AccountConfirmationTokenAddedToUserHandler> logger, EmailMessageBuilder emailMessageBuilder) : base(
        emailService, configuration, logger, emailMessageBuilder)
    {
    }

    public Task Handle(AccountConfirmationTokenAddedToUser notification, CancellationToken cancellationToken)
    {
        SendConfirmationLink(notification.UserId.ToString(), notification.Token, notification.Email);

        return Task.CompletedTask;
    }
}