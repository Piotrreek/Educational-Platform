namespace EducationalPlatform.Domain.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(string message);
}