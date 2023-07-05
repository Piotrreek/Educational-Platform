using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace EducationalPlatform.Domain.Tests;

public class UniversityTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void AddNewFaculty_ForEmptyFacultyName_ReturnsBadRequestResult(string facultyName)
    {
        // arrange
        var university = new University("name");

        // act
        var result = university.AddNewFaculty(facultyName);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(FacultyErrorMessages.EmptyName);
    }

    [Fact]
    public void AddNewFaculty_ForFacultyWithAlreadyExistingName_ReturnsBadRequestResult()
    {
        // arrange
        const string facultyName = "123";
        var university = new University("name");
        SetPropertyHelpers.SetProperty(university, "_faculties", new List<Faculty> { new(facultyName) });

        // act
        var result = university.AddNewFaculty(facultyName);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(FacultyErrorMessages.FacultyWithNameAlreadyExists);
    }

    [Fact]
    public void AddNewFaculty_ForCorrectFaculty_ReturnsSuccess()
    {
        // arrange
        var university = new University("name");
        SetPropertyHelpers.SetProperty(university, "_faculties", new List<Faculty> { new("123") });

        // act
        var result = university.AddNewFaculty("1234");

        // assert
        result.IsT0.Should().BeTrue();
        university.Faculties.Count.Should().Be(2);
        university.Faculties.Should().Contain(f => f.Name == "1234");
    }

    [Fact]
    public void AssignUser_ForAlreadyAssignedUserToOtherUniversity_ReturnsBadRequestResult()
    {
        // arrange
        var university = new University("name");
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, Guid.NewGuid());

        // act
        var result = university.AssignUser(user);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversityErrorMessages.UserAlreadyAssignedToUniversity);
    }

    [Fact]
    public void AssignUser_ForAlreadyAssignedUserToIdenticalUniversity_ReturnsBadRequestResult()
    {
        // arrange
        var university = new University("name");
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty(user, u => u.UniversityId, university.Id);

        // act
        var result = university.AssignUser(user);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversityErrorMessages.UserAlreadyInSameUniversity);
    }

    [Fact]
    public void AssignUser_ForUserWithoutAssignedUniversity_ReturnsSuccess()
    {
        // arrange
        var university = new University("name");
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());

        // act
        var result = university.AssignUser(user);

        // assert
        result.IsT0.Should().BeTrue();
        university.Users.Should().Contain(user);
    }

    [Fact]
    public void RemoveUser_ForNotExistingUserInList_ReturnsBadRequestResult()
    {
        var university = new University("name");
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty<AcademyEntity>(university, "_users", new List<User>()
        {
            new("userName1", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid()),
            new("userName2", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid())
        });

        // act
        var result = university.RemoveUser(user);

        // assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Message.Should().Be(UniversityErrorMessages.UserNotInUniversity);
    }

    [Fact]
    public void RemoveUser_ForExistingUserInList_ReturnsSuccess()
    {
        var university = new University("name");
        var user = new User("userName", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid());
        SetPropertyHelpers.SetProperty<AcademyEntity>(university, "_users", new List<User>()
        {
            new("userName1", "test@test.com", "dasdasds", "fdsfdsfsd", "123456789", Guid.NewGuid()),
            user
        });

        // act
        var result = university.RemoveUser(user);

        // assert
        result.IsT0.Should().BeTrue();
        university.Users.Should().NotContain(user);
        university.Users.Count.Should().Be(1);
    }
}