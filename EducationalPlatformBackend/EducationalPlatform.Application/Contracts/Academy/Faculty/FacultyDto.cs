using EducationalPlatform.Application.Contracts.Academy.UniversitySubject;

namespace EducationalPlatform.Application.Contracts.Academy.Faculty;

public class FacultyDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<UniversitySubjectDto> UniversitySubjects { get; init; }
}