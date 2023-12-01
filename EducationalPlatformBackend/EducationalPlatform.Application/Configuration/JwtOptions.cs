namespace EducationalPlatform.Application.Configuration;

public class JwtOptions
{
    public const string SectionName = "Authentication";
    
    public string SecretKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpireHours { get; init; }
}