using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
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
    public string? PhoneNumber { get; private set; }
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
    private readonly List<DidacticMaterial> _didacticMaterials = new();
    public IReadOnlyCollection<DidacticMaterial> DidacticMaterials => _didacticMaterials;
    private readonly List<DidacticMaterialOpinion> _didacticMaterialOpinions = new();
    public IReadOnlyCollection<DidacticMaterialOpinion> DidacticMaterialOpinions => _didacticMaterialOpinions;
    private readonly List<DidacticMaterialRating> _ratings = new();
    public IReadOnlyCollection<DidacticMaterialRating> Ratings => _ratings;

    public User(string userName, string email, string passwordHash, string salt, Guid roleId)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        RoleId = roleId;
    }

    public void AssignToUniversity(University? university)
    {
        if (UniversityId == university?.Id)
            return;

        UniversityId = university?.Id;
        University = university;
        FacultyId = null;
        Faculty = null;
        UniversitySubjectId = null;
        UniversitySubject = null;
    }

    public OneOf<Success, BadRequestResult> AssignToFaculty(Faculty? faculty)
    {
        if (faculty is null)
        {
            FacultyId = null;
            Faculty = null;
            UniversitySubject = null;
            UniversitySubjectId = null;

            return new Success();
        }

        if (!UniversityId.HasValue)
            return new BadRequestResult(FacultyErrorMessages.CannotAssignFacultyWithoutUniversity);

        if (University!.GetFacultyById(faculty.Id).IsT1)
            return new BadRequestResult(FacultyErrorMessages.FacultyWithIdNotExists);

        if (FacultyId == faculty.Id)
            return new Success();

        FacultyId = faculty.Id;
        Faculty = faculty;
        UniversitySubjectId = null;

        return new Success();
    }

    public OneOf<Success, BadRequestResult> AssignToUniversitySubject(Guid? subjectId)
    {
        if (subjectId is null)
        {
            UniversitySubjectId = null;
            UniversitySubject = null;

            return new Success();
        }

        if (!UniversityId.HasValue || !FacultyId.HasValue)
            return new BadRequestResult(UniversitySubjectErrorMessages
                .CannotAssignUniversitySubjectWithoutFacultyOrUniversityBeingSetEarlier);

        if (University!.GetUniversitySubjectById(Faculty!.Id, subjectId).IsT1)
            return new BadRequestResult(UniversitySubjectErrorMessages.SubjectInFacultyOrUniversityNotExists);

        UniversitySubjectId = subjectId;

        return new Success();
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

    public void ChangePassword(string newPasswordHash, string newSalt)
    {
        PasswordHash = newPasswordHash;
        Salt = newSalt;
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