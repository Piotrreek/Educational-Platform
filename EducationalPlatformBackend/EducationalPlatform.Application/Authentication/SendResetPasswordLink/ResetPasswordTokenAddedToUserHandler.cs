using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Builders;
using EducationalPlatform.Domain.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication.SendResetPasswordLink;

public class ResetPasswordTokenAddedToUserHandler : DomainEventHandler<ResetPasswordTokenAddedToUser>
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
        // TODO: Website address to be replaced when frontend application will exist
        // TODO: Frontend application will fetch address which is below now

        var message = _builder
            .WithMessage(
                $"To reset your password click this link: {_applicationUrl}user/reset-password/{domainEvent.UserId.ToString().ToLower()}?token={domainEvent.Token}")
            .WithRecipient(domainEvent.Email)
            .WithIsHtmlMessage(false)
            .WithSubject("Educational Platform - Reset Password Link")
            .Build();

        await _emailService.SendAsync(message);
    }
}