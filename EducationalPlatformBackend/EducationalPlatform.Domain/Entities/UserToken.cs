using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class UserToken : Entity
{
    public Guid UserId { get; }
    public string Token { get; } = null!;
    public DateTimeOffset ExpirationDateTimeOffset { get; }
    public TokenType TokenType { get; }

    public UserToken(string token, DateTimeOffset expirationDateTimeOffset, TokenType tokenType)
    {
        Token = token;
        ExpirationDateTimeOffset = expirationDateTimeOffset;
        TokenType = tokenType;
    }

    private UserToken()
    {
    }
}