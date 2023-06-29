using EducationalPlatform.Application.Abstractions;
using MediatR;

namespace EducationalPlatform.Application.Authentication;

public class AccountConfirmationTokenAddedToUser : DomainEvent
{
    public Guid UserId { get; }
    public string Token { get; }
    public string Email { get; }


    public AccountConfirmationTokenAddedToUser(Guid userId,
        string token, string email)
    {
        UserId = userId;
        Token = token;
        Email = email;
    }
};