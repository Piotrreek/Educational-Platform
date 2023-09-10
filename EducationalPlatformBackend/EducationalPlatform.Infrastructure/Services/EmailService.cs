using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EducationPlatform.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailService(IOptions<EmailConfiguration> options)
    {
        _emailConfiguration = options.Value;
    }

    public async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        client.CheckCertificateRevocation = false;
        await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
        client.Dispose();
    }
}