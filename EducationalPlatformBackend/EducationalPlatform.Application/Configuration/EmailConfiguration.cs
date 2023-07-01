namespace EducationalPlatform.Application.Configuration;

public class EmailConfiguration
{
    public string SmtpServer { get; set; }
    public string From { get; set; }
    public string FromName { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public EmailConfiguration()
    {
        SmtpServer = string.Empty;
        From = string.Empty;
        FromName = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
    }
}