namespace EducationalPlatform.Application.Contracts.Academy.UniversityCourse;

public class UniversityCourseDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string UniversityCourseSession { get; init; }
}