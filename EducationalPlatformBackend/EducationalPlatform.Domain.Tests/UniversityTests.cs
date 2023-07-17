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
}