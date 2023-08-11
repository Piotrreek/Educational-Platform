using EducationalPlatform.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace EducationPlatform.Infrastructure.Services;

public class DevNotificationsSender : IEmailService
{
    private readonly ILogger<DevNotificationsSender> _logger;

    public DevNotificationsSender(ILogger<DevNotificationsSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(MimeMessage message)
    {
        _logger.LogInformation(message.TextBody);

        return Task.CompletedTask;
    }
}