using System.Reflection;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EducationalPlatform.Domain.Tests;

public class UserTests
{
    [Fact]
    public void ConfirmAccount_ForConfirmedAccount_ShouldReturnBadRequestResult()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.EmailConfirmed, true);

        // act
        var result = user.ConfirmAccount(string.Empty, DateTimeOffset.Now);

        // assert
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public void ConfirmAccount_ForInvalidToken_ShouldReturnBadRequestResult()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", GetExampleDateTimeOffSet(),
                    TokenType.ResetPasswordToken)
            });

        // act
        var result = user.ConfirmAccount("token", GetExampleDateTimeOffSet().AddHours(-1));

        // assert
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public void ConfirmAccount_ForExpiredToken_ShouldReturnBadRequestResult()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", GetExampleDateTimeOffSet(),
                    TokenType.AccountConfirmationToken)
            });

        // act
        var result = user.ConfirmAccount("token", GetExampleDateTimeOffSet().AddHours(1));

        // assert
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public void ConfirmAccount_ForValidToken_ShouldReturnSuccess()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", GetExampleDateTimeOffSet(),
                    TokenType.AccountConfirmationToken)
            });

        // act
        var result = user.ConfirmAccount("token", GetExampleDateTimeOffSet().AddHours(-1));

        // assert
        result.IsT0.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AddUserLogin_ForSuccessfulLogin_AddsNewSuccessfulUserLogin(bool isSuccess)
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());

        // act
        user.AddLoginAttempt(isSuccess);

        // assert
        user.UserLogins.Count.Should().Be(1);
        user.UserLogins.Should().Contain(ul => ul.IsSuccess == isSuccess);
    }

    [Theory]
    [InlineData("token", TokenType.AccountConfirmationToken)]
    [InlineData("token", TokenType.ResetPasswordToken)]
    public void AddUserToken_ForAccountConfirmationToken_AddsCorrectTokenToList(string token, TokenType tokenType)
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());

        // act
        user.AddUserToken(token, tokenType);

        // assert
        user.UserTokens.Count.Should().Be(1);
        user.UserTokens.Should()
            .Contain(ut => ut.Token == token && ut.TokenType == tokenType);
    }

    [Theory]
    [MemberData(nameof(GetValidTokens))]
    public void IsUserTokenValid_ReturnsTrue_ForCorrectToken(TokenType tokenType, string token,
        DateTimeOffset dateTimeOffset)
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", GetExampleDateTimeOffSet().AddDays(1),
                    TokenType.AccountConfirmationToken),
                new("token2", GetExampleDateTimeOffSet().AddHours(5),
                    TokenType.ResetPasswordToken),
                new("token3", GetExampleDateTimeOffSet().AddMonths(1),
                    TokenType.AccountConfirmationToken)
            });
        var methodInfo = GetIsUserTokenValidMethodInfo(user);

        // act
        var result = (bool)methodInfo.Invoke(user, new object[] { tokenType, token, dateTimeOffset })!;

        // assert
        result.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(GetInvalidTokens))]
    public void IsUserTokenValid_ReturnsFalse_ForIncorrectToken(TokenType tokenType, string token,
        DateTimeOffset dateTimeOffset)
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", GetExampleDateTimeOffSet().AddDays(1),
                    TokenType.AccountConfirmationToken),
                new("token2", GetExampleDateTimeOffSet().AddHours(5),
                    TokenType.ResetPasswordToken),
                new("token3", GetExampleDateTimeOffSet().AddMonths(1),
                    TokenType.AccountConfirmationToken)
            });
        var methodInfo = GetIsUserTokenValidMethodInfo(user);

        // act
        var result = (bool)methodInfo.Invoke(user, new object[] { tokenType, token, dateTimeOffset })!;

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ChangeExpirationDateOfTokens_CorrectlyChangesExpirationDateForSpecificTokens()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var exampleDateTimeOffSet = GetExampleDateTimeOffSet();
        const TokenType tokenType = TokenType.AccountConfirmationToken;
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", exampleDateTimeOffSet.AddDays(1),
                    TokenType.AccountConfirmationToken),
                new("token2", exampleDateTimeOffSet.AddHours(5),
                    TokenType.ResetPasswordToken),
                new("token3", exampleDateTimeOffSet.AddMonths(1),
                    TokenType.AccountConfirmationToken)
            });

        // act
        user.ChangeExpirationDateOfUserTokensOfGivenType(tokenType, exampleDateTimeOffSet);

        // assert
        user.UserTokens.Where(ut => ut.TokenType == tokenType).Should()
            .AllSatisfy(ut => { ut.ExpirationDateTimeOffset.Should().Be(exampleDateTimeOffSet); });
        user.UserTokens.First(ut => ut.TokenType != TokenType.AccountConfirmationToken).ExpirationDateTimeOffset
            .Should()
            .Be(exampleDateTimeOffSet.AddHours(5));
    }

    [Fact]
    public void ResetPassword_ForCorrectToken_ChangesPasswordHashAndSalt()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var exampleDateTimeOffSet = GetExampleDateTimeOffSet();
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", exampleDateTimeOffSet.AddDays(1),
                    TokenType.AccountConfirmationToken),
                new("token2", exampleDateTimeOffSet.AddHours(5),
                    TokenType.ResetPasswordToken),
                new("token3", exampleDateTimeOffSet.AddMonths(1),
                    TokenType.AccountConfirmationToken)
            });

        // act
        var result = user.ResetPassword("123", "1234", "token2", exampleDateTimeOffSet.AddHours(3));

        // assert
        result.IsT0.Should().BeTrue();
        user.PasswordHash.Should().Be("123");
        user.Salt.Should().Be("1234");
    }

    [Fact]
    public void ResetPassword_ForExpiredToken_ReturnBadRequestResult()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var exampleDateTimeOffSet = GetExampleDateTimeOffSet();
        SetPropertyHelpers.SetProperty(user, u => u.UserTokens,
            new List<UserToken>()
            {
                new("token", exampleDateTimeOffSet.AddDays(1),
                    TokenType.AccountConfirmationToken),
                new("token2", exampleDateTimeOffSet.AddHours(5),
                    TokenType.ResetPasswordToken),
                new("token3", exampleDateTimeOffSet.AddMonths(1),
                    TokenType.AccountConfirmationToken)
            });

        // act
        var result = user.ResetPassword("123", "1234", "token2", exampleDateTimeOffSet.AddHours(6));

        // assert
        result.IsT1.Should().BeTrue();
        user.PasswordHash.Should().Be("dasdasds");
        user.Salt.Should().Be("fdsfdsfsd");
    }

    [Fact]
    public void ResetPassword_ForNotExistingToken_ReturnsBadRequestResult()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var exampleDateTimeOffSet = GetExampleDateTimeOffSet();

        // act
        var result = user.ResetPassword("123", "1234", "token", exampleDateTimeOffSet);

        // assert
        result.IsT1.Should().BeTrue();
        user.PasswordHash.Should().Be("dasdasds");
        user.Salt.Should().Be("fdsfdsfsd");
    }

    [Fact]
    public void AssignToUniversity_ForNull_MakesAllAcademyFieldsNull()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, Guid.NewGuid());

        // act
        user.AssignToUniversity(null);

        // act
        user.UniversityId.Should().BeNull();
        user.FacultyId.Should().BeNull();
        user.UniversitySubjectId.Should().BeNull();
    }

    [Fact]
    public void AssignToUniversity_ForEqualUniversity_DoesNotChangeAnything()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var universityId = Guid.NewGuid();
        var facultyId = Guid.NewGuid();
        var subjectId = Guid.NewGuid();
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, universityId);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, facultyId);
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, subjectId);
        var university = new University("test");
        SetPropertyHelpers.SetProperty(university, u => u.Id, universityId);

        // act
        user.AssignToUniversity(university);

        // assert
        user.UniversityId.Should().Be(universityId);
        user.FacultyId.Should().Be(facultyId);
        user.UniversitySubjectId.Should().Be(subjectId);
    }

    [Fact]
    public void AssignToUniversity_ForDifferentUniversity_ChangesUniversityIdAndMakesOtherAcademyPropertiesNull()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var universityId = Guid.NewGuid();
        var facultyId = Guid.NewGuid();
        var subjectId = Guid.NewGuid();
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, universityId);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, facultyId);
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, subjectId);
        var university = new University("test");

        // act
        user.AssignToUniversity(university);

        // assert
        user.UniversityId.Should().Be(university.Id);
        user.FacultyId.Should().BeNull();
        user.UniversitySubjectId.Should().BeNull();
    }

    [Fact]
    public void AssignToFaculty_ForNull_MakesFacultyIdAndSubjectIdEqualToNull()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var universityId = Guid.NewGuid();
        var facultyId = Guid.NewGuid();
        var subjectId = Guid.NewGuid();
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, universityId);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, facultyId);
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, subjectId);

        // act
        user.AssignToFaculty(null);

        // assert
        user.UniversityId.Should().Be(universityId);
        user.FacultyId.Should().BeNull();
        user.UniversitySubjectId.Should().BeNull();
    }

    [Fact]
    public void AssignToFaculty_WhenUniversityIdIsNullAndGivenFacultyIsNotNull_ReturnsBadRequest()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var faculty = new Faculty("test");

        // act
        var result = user.AssignToFaculty(faculty);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(FacultyErrorMessages.CannotAssignFacultyWithoutUniversity);
    }

    [Fact]
    public void AssignToFaculty_ForNotExistingFacultyInUniversity_ReturnsBadRequest()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var university = new University("test");
        var faculty = new Faculty("test");
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);
        SetPropertyHelpers.SetProperty(user, u => u.University, university);

        // act
        var result = user.AssignToFaculty(faculty);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(FacultyErrorMessages.FacultyWithIdNotExists);
    }

    [Fact]
    public void AssignToFaculty_ForEqualFaculty_ReturnsSuccessAndDoesNotChangeAnything()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var university = new University("test");
        var faculty = new Faculty("test");
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);
        SetPropertyHelpers.SetProperty(user, u => u.University, university);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, faculty.Id);
        SetPropertyHelpers.SetProperty(university, "_faculties", new List<Faculty> { faculty });

        // act
        var result = user.AssignToFaculty(faculty);

        // assert
        result.IsT0.Should().BeTrue();
    }

    [Fact]
    public void AssignToFaculty_ForDifferentFaculty_ReturnsSuccessAndChangesFacultyIdToNewIdAndSubjectIdToNull()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var university = new University("test");
        var faculty = new Faculty("test");
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);
        SetPropertyHelpers.SetProperty(user, u => u.University, university);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, faculty.Id);
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, Guid.NewGuid());
        var newFaculty = new Faculty("test2");
        SetPropertyHelpers.SetProperty(university, "_faculties", new List<Faculty> { newFaculty });

        // act
        var result = user.AssignToFaculty(newFaculty);

        // assert
        result.IsT0.Should().BeTrue();
        user.FacultyId.Should().Be(newFaculty.Id);
        user.UniversitySubjectId.Should().BeNull();
    }

    [Fact]
    public void AssignToUniversitySubject_ForNull_ReturnsSuccessAndChangesCurrentSubjectIdToNull()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var subject = new UniversitySubject("test", UniversitySubjectDegree.First);
        SetPropertyHelpers.SetProperty(user, u => u.UniversitySubjectId, subject.Id);

        // act
        var result = user.AssignToUniversitySubject(null);

        // assert
        result.IsT0.Should().BeTrue();
        user.UniversitySubjectId.Should().BeNull();
    }

    [Fact]
    public void AssignToUniversitySubject_WhenUniversityIdIsNotSet_ReturnsBadRequest()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, Guid.NewGuid());

        // act
        var result = user.AssignToUniversitySubject(Guid.NewGuid());

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversitySubjectErrorMessages
            .CannotAssignUniversitySubjectWithoutFacultyOrUniversityBeingSetEarlier);
    }

    [Fact]
    public void AssignToUniversitySubject_WhenFacultyIdIsNotSet_ReturnsBadRequest()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, Guid.NewGuid());

        // act
        var result = user.AssignToUniversitySubject(Guid.NewGuid());

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversitySubjectErrorMessages
            .CannotAssignUniversitySubjectWithoutFacultyOrUniversityBeingSetEarlier);
    }

    [Fact]
    public void AssignToUniversitySubject_WhenSubjectWithGivenIdIsNotInFaculty_ReturnsBadRequest()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var university = new University("test");
        var faculty = new Faculty("test");
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, faculty.Id);
        SetPropertyHelpers.SetProperty(user, u => u.University, university);
        SetPropertyHelpers.SetProperty(user, u => u.Faculty, faculty);

        // act
        var result = user.AssignToUniversitySubject(Guid.NewGuid());

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversitySubjectErrorMessages.SubjectInFacultyOrUniversityNotExists);
    }

    [Fact]
    public void AssignToUniversitySubject_ForCorrectSubject_ModifiesId()
    {
        // arrange
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", Guid.NewGuid());
        var university = new University("test");
        var faculty = new Faculty("test");
        var subject = new UniversitySubject("test", UniversitySubjectDegree.First);
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);
        SetPropertyHelpers.SetProperty(user, u => u.FacultyId, faculty.Id);
        SetPropertyHelpers.SetProperty(user, u => u.University, university);
        SetPropertyHelpers.SetProperty(user, u => u.Faculty, faculty);
        SetPropertyHelpers.SetProperty(university, "_faculties", new List<Faculty> { faculty });
        SetPropertyHelpers.SetProperty(faculty, "_universitySubjects", new List<UniversitySubject> { subject });

        // act
        var result = user.AssignToUniversitySubject(subject.Id);

        // assert
        result.IsT0.Should().BeTrue();
        user.UniversityId.Should().Be(university.Id);
        user.FacultyId.Should().Be(faculty.Id);
        user.UniversitySubjectId.Should().Be(subject.Id);
    }

    public static IEnumerable<object[]> GetValidTokens()
    {
        return new List<object[]>
        {
            new object[]
            {
                TokenType.AccountConfirmationToken,
                "token3",
                GetExampleDateTimeOffSet().AddMonths(1).AddDays(-1)
            },
            new object[]
            {
                TokenType.ResetPasswordToken,
                "token2",
                GetExampleDateTimeOffSet().AddHours(4)
            },
        };
    }

    public static IEnumerable<object[]> GetInvalidTokens()
    {
        return new List<object[]>
        {
            new object[]
            {
                TokenType.AccountConfirmationToken,
                "token3",
                GetExampleDateTimeOffSet().AddMonths(1).AddDays(1)
            },
            new object[]
            {
                TokenType.AccountConfirmationToken,
                "token2",
                GetExampleDateTimeOffSet().AddHours(4)
            },
            new object[]
            {
                TokenType.AccountConfirmationToken,
                "token5",
                GetExampleDateTimeOffSet().AddHours(4)
            }
        };
    }

    private static DateTimeOffset GetExampleDateTimeOffSet()
    {
        return new DateTimeOffset(2023, 6, 28, 14, 10, 0, TimeSpan.Zero);
    }

    private static MethodInfo GetIsUserTokenValidMethodInfo(User user)
    {
        return user.GetType().GetMethod("IsUserTokenValid", BindingFlags.NonPublic | BindingFlags.Instance)!;
    }
}