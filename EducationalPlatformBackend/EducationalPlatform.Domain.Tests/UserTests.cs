using System.Linq.Expressions;
using System.Reflection;
using EducationalPlatform.Domain.Entities;
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
        SetProperty(user, u => u.EmailConfirmed, true);

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
        SetProperty(user, u => u.UserTokens,
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
        SetProperty(user, u => u.UserTokens,
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
        SetProperty(user, u => u.UserTokens,
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
        SetProperty(user, u => u.UserTokens,
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
        SetProperty(user, u => u.UserTokens,
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

    private static void SetProperty<TProperty>(User user, Expression<Func<User, TProperty>> property, TProperty value)
    {
        var propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;
        propertyInfo.SetValue(user, value);
    }

    private static MethodInfo GetIsUserTokenValidMethodInfo(User user)
    {
        return user.GetType().GetMethod("IsUserTokenValid", BindingFlags.NonPublic | BindingFlags.Instance)!;
    }
}