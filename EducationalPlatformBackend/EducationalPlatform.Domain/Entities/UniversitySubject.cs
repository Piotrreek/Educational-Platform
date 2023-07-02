using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class UniversitySubject : Entity
{
    public string Name { get; private set; } = null!;
    public UniversitySubjectDegree UniversitySubjectDegree { get; private set; }
    public Faculty Faculty { get; private set; } = null!;
    public Guid FacultyId { get; private set; }
    private readonly List<UniversityCourse> _universityCourses = new();
    public IReadOnlyCollection<UniversityCourse> UniversityCourses => _universityCourses;
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public UniversitySubject(string name, UniversitySubjectDegree universitySubjectDegree)
    {
        Name = name;
        UniversitySubjectDegree = universitySubjectDegree;
    }

    // For EF
    private UniversitySubject()
    {
    }
}