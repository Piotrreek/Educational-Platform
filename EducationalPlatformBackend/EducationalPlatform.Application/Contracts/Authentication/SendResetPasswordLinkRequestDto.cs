namespace EducationalPlatform.Application.Contracts.Authentication;

public class SendResetPasswordLinkRequestDto
{
    public string Email { get; set; }

    public SendResetPasswordLinkRequestDto()
    {
        Email = string.Empty;
    }
}