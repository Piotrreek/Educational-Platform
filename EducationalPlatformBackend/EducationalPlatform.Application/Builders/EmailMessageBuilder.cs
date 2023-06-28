using EducationalPlatform.Application.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EducationalPlatform.Application.Builders;

public class EmailMessageBuilder
{
    private string[] _recipients = Array.Empty<string>();
    private readonly string _sender;
    private readonly string _senderName;
    private string _message = string.Empty;
    private string _subject = string.Empty;
    private bool _isHtmlMessage;

    public EmailMessageBuilder(IOptions<EmailConfiguration> options)
    {
        _sender = options.Value.From;
        _senderName = options.Value.FromName;
    }

    public EmailMessageBuilder WithRecipients(string[] recipients)
    {
        _recipients = recipients;
        return this;
    }

    public EmailMessageBuilder WithRecipient(string recipient)
    {
        _recipients = new[] { recipient };
        return this;
    }

    public EmailMessageBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public EmailMessageBuilder WithIsHtmlMessage(bool isHtmlMessage)
    {
        _isHtmlMessage = isHtmlMessage;
        return this;
    }

    public EmailMessageBuilder WithSubject(string subject)
    {
        _subject = subject;
        return this;
    }

    public MimeMessage Build()
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_senderName, _sender));
        emailMessage.To.AddRange(_recipients.Select(r => new MailboxAddress(r, r)));
        emailMessage.Subject = _subject;
        emailMessage.Body = new TextPart(_isHtmlMessage ? TextFormat.Html : TextFormat.Text) { Text = _message };

        return emailMessage;
    }
}