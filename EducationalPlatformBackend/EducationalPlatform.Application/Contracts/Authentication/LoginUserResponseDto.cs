namespace EducationalPlatform.Application.Contracts.Authentication;

public class LoginUserResponseDto
{
    public string Token { get; set; }

    public LoginUserResponseDto(string token)
    {
        Token = token;
    }
}