using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class UserToken : Entity
{
    public Guid UserId { get; }
    public string Token { get; } = null!;
    public DateTimeOffset ExpirationDateTimeOffset { get; }
    public TokenType TokenType { get; }

    public UserToken(Guid userId, string token, DateTimeOffset expirationDateTimeOffset, TokenType tokenType)
    {
        UserId = userId;
        Token = token;
        ExpirationDateTimeOffset = expirationDateTimeOffset;
        TokenType = tokenType;
    }

    private UserToken()
    {
    }
}