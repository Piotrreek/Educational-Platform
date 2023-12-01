using EducationalPlatform.Application.Abstractions;

namespace EducationalPlatform.Application.Authentication.Commands.SendResetPasswordLink;

public class ResetPasswordTokenAddedToUser : DomainEvent
{
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }

    public ResetPasswordTokenAddedToUser(string email, Guid userId, string token)
    {
        Email = email;
        UserId = userId;
        Token = token;
    }

}