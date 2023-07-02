using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class Faculty : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<UniversitySubject> _universitySubjects = new();
    public IReadOnlyCollection<UniversitySubject> UniversitySubjects => _universitySubjects;
    public University University { get; private set; } = null!;
    public Guid UniversityId { get; private set; }
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public Faculty(string name)
    {
        Name = name;
    }

    // For EF
    private Faculty()
    {
    }
}