using EducationalPlatform.Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace EducationPlatform.Infrastructure.Services;

public class DevNotificationsSender : IEmailService
{
    private readonly Logger<DevNotificationsSender> _logger;

    public DevNotificationsSender(Logger<DevNotificationsSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string message)
    {
        _logger.LogInformation(message);

        return Task.CompletedTask;
    }
}