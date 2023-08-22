namespace EducationalPlatform.Application.Contracts.Authentication;

public class ResetPasswordRequestDto
{
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}