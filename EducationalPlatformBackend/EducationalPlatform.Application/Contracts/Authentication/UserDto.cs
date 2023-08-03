namespace EducationalPlatform.Application.Contracts.Authentication;

public class UserDto
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required bool EmailConfirmed { get; init; }
    public string? PhoneNumber { get; init; }
    public required string? UniversityName { get; init; }
    public required string? FacultyName { get; init; }
    public required string? UniversitySubjectName { get; init; }
}