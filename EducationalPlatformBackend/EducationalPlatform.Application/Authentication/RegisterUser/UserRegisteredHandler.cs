using EducationalPlatform.Domain.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class UserRegisteredHandler : INotificationHandler<UserRegistered>
{
    private readonly IEmailService _emailService;
    private readonly string _applicationUrl;
    private readonly ILogger<UserRegisteredHandler> _logger;

    public UserRegisteredHandler(IEmailService emailService, IConfiguration configuration,
        ILogger<UserRegisteredHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
        _applicationUrl = configuration["ApplicationUrl"]!;
    }

    public Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var message =
            $"Confirm your account by clicking this link: {_applicationUrl}user/confirm/{notification.UserId.ToString()}?token={notification.Token}";

        Task.Run(async () =>
        {
            var result = await Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30))
                .ExecuteAndCaptureAsync(async () => await _emailService.SendAsync(message, notification.Email));

            if (result.Outcome == OutcomeType.Successful)
                _logger.LogInformation(@"Activation link was sent to user with email {email}", notification.Email);
            else
                _logger.LogError(
                    @"Activation link could not be sent to user with email {email}. The exception message: {message}",
                    notification.Email, result.FinalException.Message);
        }, cancellationToken);

        return Task.CompletedTask;
    }
}