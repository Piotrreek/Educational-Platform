namespace EducationalPlatform.Application.Configuration;

public class JwtOptions
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int ExpireHours { get; init; }

    public JwtOptions()
    {
        SecretKey = string.Empty;
        Issuer = string.Empty;
        Audience = string.Empty;
    }
}