namespace EducationalPlatform.Application.Contracts.Authentication;

public class RegisterUserRequestDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string RequestedRoleName { get; set; }

    public RegisterUserRequestDto()
    {
        Username = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        RequestedRoleName = string.Empty;
    }
}