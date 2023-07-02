using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class UniversityCourse : Entity
{
    public string Name { get; private set; } = null!;
    public UniversityCourseYear UniversityCourseYear { get; private set; }
    public UniversitySubject UniversitySubject { get; private set; } = null!;
    public Guid UniversitySubjectId { get; private set; }
}