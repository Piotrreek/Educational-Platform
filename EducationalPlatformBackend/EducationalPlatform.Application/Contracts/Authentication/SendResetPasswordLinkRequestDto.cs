namespace EducationalPlatform.Application.Contracts.Authentication;

public class SendResetPasswordLinkRequestDto
{
    public string Email { get; set; } = string.Empty;
}