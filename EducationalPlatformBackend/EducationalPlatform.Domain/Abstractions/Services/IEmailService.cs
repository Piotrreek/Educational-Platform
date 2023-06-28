using MimeKit;

namespace EducationalPlatform.Domain.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(MimeMessage message);
}