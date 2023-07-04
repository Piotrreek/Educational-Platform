using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public sealed class User : Entity
{
    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool EmailConfirmed { get; private set; }
    public string PasswordHash { get; private set; } = null!;
    public string Salt { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
    public Guid RoleId { get; private set; }
    public University? University { get; private set; }
    public Guid? UniversityId { get; private set; }
    public Faculty? Faculty { get; private set; }
    public Guid? FacultyId { get; private set; }
    public UniversitySubject? UniversitySubject { get; private set; }
    public Guid? UniversitySubjectId { get; private set; }
    public ICollection<UserLogin> UserLogins { get; private set; } = new List<UserLogin>();
    public ICollection<UserToken> UserTokens { get; private set; } = new List<UserToken>();

    public User(string userName, string email, string passwordHash, string salt, string phoneNumber,
        Guid roleId)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        PhoneNumber = phoneNumber;
        RoleId = roleId;
    }

    public OneOf<Success, BadRequestResult> ConfirmAccount(string token, DateTimeOffset confirmationDate)
    {
        if (EmailConfirmed)
            return new BadRequestResult(GeneralErrorMessages.AccountAlreadyConfirmedErrorMessage);

        if (!IsUserTokenValid(TokenType.AccountConfirmationToken, token, confirmationDate))
            return new BadRequestResult(GeneralErrorMessages.BadAccountConfirmationLinkMessage);

        EmailConfirmed = true;

        return new Success();
    }

    public void AddLoginAttempt(bool isSuccess)
    {
        UserLogins.Add(new UserLogin(isSuccess));
    }

    public void AddUserToken(string tokenValue, TokenType tokenType)
    {
        UserTokens.Add(new UserToken(tokenValue, DateTimeOffset.Now.AddDays(1), tokenType));
    }

    public void ChangeExpirationDateOfUserTokensOfGivenType(TokenType tokenType, DateTimeOffset expirationTimeBoundary)
    {
        var tokens = UserTokens
            .Where(t => t.TokenType == tokenType && t.ExpirationDateTimeOffset >= expirationTimeBoundary)
            .ToList();

        foreach (var token in tokens)
        {
            token.ChangeExpirationDate(expirationTimeBoundary);
        }
    }

    public OneOf<Success, BadRequestResult> ResetPassword(string newPasswordHash, string newSalt, string token,
        DateTimeOffset date)
    {
        if (!IsUserTokenValid(TokenType.ResetPasswordToken, token, date))
            return new BadRequestResult(GeneralErrorMessages.BadResetPasswordLinkMessage);

        PasswordHash = newPasswordHash;
        Salt = newSalt;

        return new Success();
    }

    private bool IsUserTokenValid(TokenType tokenType, string token, DateTimeOffset date) =>
        UserTokens.Any(ut =>
            ut.Token == token &&
            ut.TokenType == tokenType &&
            ut.ExpirationDateTimeOffset >= date);

    // For EF
    private User()
    {
    }
}