namespace EducationalPlatform.Application.Contracts.Authentication;

public class ResetPasswordRequestDto
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public ResetPasswordRequestDto()
    {
        Password = string.Empty;
        ConfirmPassword = string.Empty;
    }
}