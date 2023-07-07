using System.Reflection;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());

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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());

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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
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
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        var exampleDateTimeOffSet = GetExampleDateTimeOffSet();
        
        // act
        var result = user.ResetPassword("123", "1234", "token", exampleDateTimeOffSet);
        
        // assert
        result.IsT1.Should().BeTrue();
        user.PasswordHash.Should().Be("dasdasds");
        user.Salt.Should().Be("fdsfdsfsd");
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