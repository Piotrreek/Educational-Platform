using MediatR;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class UserRegistered : INotification
{
    public Guid UserId { get; }
    public string Email { get; }
    public string Token { get; }
    
    public UserRegistered(Guid userId, string email, string token)
    {
        UserId = userId;
        Email = email;
        Token = token;
    }
}