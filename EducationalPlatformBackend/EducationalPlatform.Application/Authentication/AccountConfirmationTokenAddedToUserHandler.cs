using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication;

internal sealed class AccountConfirmationTokenAddedToUserHandler : DomainEventHandler<AccountConfirmationTokenAddedToUser>
{
    private readonly IEmailService _emailService;
    private readonly string _applicationUrl;
    private readonly EmailMessageBuilder _builder;

    public AccountConfirmationTokenAddedToUserHandler(IEmailService emailService, IConfiguration configuration,
        ILogger<AccountConfirmationTokenAddedToUserHandler> logger,
        EmailMessageBuilder builder) : base(logger)
    {
        _emailService = emailService;
        _applicationUrl = configuration["ApplicationUrl"]!;
        _builder = builder;
    }

    protected override async Task Handle(AccountConfirmationTokenAddedToUser domainEvent)
    {
        var message = _builder
            .WithMessage(
                $"Confirm your account by clicking this link: {_applicationUrl}/confirm-account/{domainEvent.UserId.ToString().ToLower()}?token={domainEvent.Token}")
            .WithRecipient(domainEvent.Email)
            .WithIsHtmlMessage(false)
            .WithSubject("Educational Platform - Account Activation Link")
            .Build();

        await _emailService.SendAsync(message);
    }
}