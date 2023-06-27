using EducationalPlatform.Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace EducationPlatform.Infrastructure.Services;

public class DevNotificationsSender : IEmailService
{
    private readonly ILogger<DevNotificationsSender> _logger;

    public DevNotificationsSender(ILogger<DevNotificationsSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string message, string email)
    {
        _logger.LogInformation(message);

        return Task.CompletedTask;
    }
}