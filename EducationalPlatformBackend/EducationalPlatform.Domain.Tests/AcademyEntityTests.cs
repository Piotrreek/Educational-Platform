using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EducationalPlatform.Domain.Tests;

public class AcademyEntityTests
{
    [Fact]
    public void AssignUser_ForAlreadyAssignedUser_ReturnsBadRequestResult()
    {
        // arrange
        var academicEntity = new AcademyEntityMock { UserAlreadyAssignedToAcademyEntityProperty = true };
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());

        // act
        var result = academicEntity.AssignUser(user);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be("123");
    }

    [Fact]
    public void AssignUser_ForAssignedUserToIdenticalAcademyEntity_ReturnsBadRequestResult()
    {
        // arrange
        var academicEntity = new AcademyEntityMock();
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty<AcademyEntity>(academicEntity, "_users", new List<User> { user });

        // act
        var result = academicEntity.AssignUser(user);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be("1234");
    }

    [Fact]
    public void AssignUser_ForCorrectUser_ShouldReturnSuccess()
    {
        // arrange
        var academicEntity = new AcademyEntityMock();
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());

        // act
        var result = academicEntity.AssignUser(user);

        // assert
        result.IsT0.Should().BeTrue();
        academicEntity.Users.Should().Contain(user);
    }

    [Fact]
    public void RemoveUser_ForUserNotInList_ReturnsBadRequestResult()
    {
        var academicEntity = new AcademyEntityMock();
        var user1 = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        var user2 = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty<AcademyEntity>(academicEntity, "_users", new List<User> { user1 });
        
        // act
        var result = academicEntity.RemoveUser(user2);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be("12345");
    }
    
    [Fact]
    public void RemoveUser_ForExistingUserInList_ReturnsSuccess()
    {
        var academicEntity = new AcademyEntityMock();
        var user1 = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        var user2 = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty<AcademyEntity>(academicEntity, "_users", new List<User> { user1, user2 });
        
        // act
        var result = academicEntity.RemoveUser(user1);

        // assert
        result.IsT0.Should().BeTrue();
        academicEntity.Users.Should().NotContain(user1);
        academicEntity.Users.Should().Contain(user2);
    }
}