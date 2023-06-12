namespace EducationalPlatform.Application.Contracts.Authentication;

public class LoginUserRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginUserRequestDto()
    {
        Email = string.Empty;
        Password = string.Empty;
    }
}