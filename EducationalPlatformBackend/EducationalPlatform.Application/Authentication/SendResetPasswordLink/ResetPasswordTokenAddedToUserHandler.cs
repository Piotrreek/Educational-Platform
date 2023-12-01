using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication.SendResetPasswordLink;

internal sealed class ResetPasswordTokenAddedToUserHandler : DomainEventHandler<ResetPasswordTokenAddedToUser>
{
    private readonly IEmailService _emailService;
    private readonly string _applicationUrl;
    private readonly EmailMessageBuilder _builder;

    public ResetPasswordTokenAddedToUserHandler(ILogger<DomainEventHandler<ResetPasswordTokenAddedToUser>> logger,
        IEmailService emailService, IConfiguration configuration, EmailMessageBuilder builder) :
        base(logger)
    {
        _emailService = emailService;
        _applicationUrl = configuration["ApplicationUrl"]!;
        _builder = builder;
    }

    protected override async Task Handle(ResetPasswordTokenAddedToUser domainEvent)
    {
        var message = _builder
            .WithMessage(
                $"To reset your password click this link: {_applicationUrl}/reset-password/{domainEvent.UserId.ToString().ToLower()}?token={domainEvent.Token}")
            .WithRecipient(domainEvent.Email)
            .WithIsHtmlMessage(false)
            .WithSubject("Educational Platform - Reset Password Link")
            .Build();

        await _emailService.SendAsync(message);
    }
}