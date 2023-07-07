using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class UserToken : Entity
{
    public Guid UserId { get; }
    public string Token { get; } = null!;
    public DateTimeOffset ExpirationDateTimeOffset { get; private set; }
    public TokenType TokenType { get; }

    public UserToken(string token, DateTimeOffset expirationDateTimeOffset, TokenType tokenType)
    {
        Token = token;
        ExpirationDateTimeOffset = expirationDateTimeOffset;
        TokenType = tokenType;
    }

    public void ChangeExpirationDate(DateTimeOffset newExpirationDate)
    {
        ExpirationDateTimeOffset = newExpirationDate;
    }

    private UserToken()
    {
    }
}