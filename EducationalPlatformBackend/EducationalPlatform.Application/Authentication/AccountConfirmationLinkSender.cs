using EducationalPlatform.Application.Builders;
using EducationalPlatform.Domain.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;

namespace EducationalPlatform.Application.Authentication;

public abstract class AccountConfirmationLinkSender
{
    private readonly IEmailService _emailService;
    private readonly string _applicationUrl;
    private readonly ILogger<AccountConfirmationLinkSender> _logger;
    private readonly EmailMessageBuilder _builder;

    protected AccountConfirmationLinkSender(IEmailService emailService, IConfiguration configuration,
        ILogger<AccountConfirmationLinkSender> logger, EmailMessageBuilder builder)
    {
        _emailService = emailService;
        _applicationUrl = configuration["ApplicationUrl"]!;
        _logger = logger;
        _builder = builder;
    }

    protected void SendConfirmationLink(string userId, string token, string email)
    {
        // TODO: Website address to be replaced when frontend application will exist
        // TODO: Frontend application will fetch address which is below now

        var message =
            _builder
                .WithMessage($"Confirm your account by clicking this link: {_applicationUrl}user/confirm/{userId.ToLower()}?token={token}")
                .WithRecipient(email)
                .WithIsHtmlMessage(false)
                .WithSubject("Educational Platform - Account Activation Link")
                .Build();

        Task.Run(async () =>
        {
            var result = await Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30))
                .ExecuteAndCaptureAsync(async () => await _emailService.SendAsync(message));

            if (result.Outcome == OutcomeType.Successful)
                _logger.LogInformation(@"Activation link was sent to user with email {email}", email);
            else
                _logger.LogError(
                    @"Activation link could not be sent to user with email {email}. The exception message: {message}",
                    email, result.FinalException.Message);
        });
    }
}