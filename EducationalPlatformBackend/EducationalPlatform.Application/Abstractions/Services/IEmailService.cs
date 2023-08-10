using MimeKit;

namespace EducationalPlatform.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(MimeMessage message);
}