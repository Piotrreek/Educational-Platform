namespace EducationalPlatform.Application.Configuration;

public class EmailConfiguration
{
    public const string SectionName = "Mailing";
    
    public string SmtpServer { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}