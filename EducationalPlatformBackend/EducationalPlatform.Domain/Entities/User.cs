using EducationalPlatform.Domain.Exceptions;
using EducationalPlatform.Domain.Primitives;

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
    public ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
    public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    public User(string userName, string email, string passwordHash, string salt, string phoneNumber, Guid roleId) : base()
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        PhoneNumber = phoneNumber;
        RoleId = roleId;
    }

    public void ConfirmEmail()
    {
        if (EmailConfirmed)
            throw new EmailAlreadyConfirmedException(ErrorMessages.EmailAlreadyConfirmedErrorMessage);

        EmailConfirmed = true;
    }

    private User()
    {
    }
}