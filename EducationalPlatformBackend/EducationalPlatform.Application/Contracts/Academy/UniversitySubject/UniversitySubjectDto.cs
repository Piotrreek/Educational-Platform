using EducationalPlatform.Application.Contracts.Academy.UniversityCourse;

namespace EducationalPlatform.Application.Contracts.Academy.UniversitySubject;

public class UniversitySubjectDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string UniversitySubjectDegree { get; init; }
    public required IEnumerable<UniversityCourseDto> UniversityCourses { get; init; }
}