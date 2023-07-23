using EducationalPlatform.Application.Contracts.Academy.Faculty;

namespace EducationalPlatform.Application.Contracts.Academy.University;

public class UniversityDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<FacultyDto> Faculties { get; init; }
}