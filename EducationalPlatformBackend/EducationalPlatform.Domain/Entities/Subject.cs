using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class Subject : Entity
{
    public string Name { get; private set; } = null!;
    public SubjectDegree SubjectDegree { get; private set; }
    public SubjectYear SubjectYear { get; private set; }
    public Faculty Faculty { get; private set; } = null!;
    public Guid FacultyId { get; private set; }

    public Subject(string name, SubjectDegree subjectDegree, SubjectYear subjectYear)
    {
        Name = name;
        SubjectDegree = subjectDegree;
        SubjectYear = subjectYear;
    }

    // For EF
    private Subject()
    {
    }
}