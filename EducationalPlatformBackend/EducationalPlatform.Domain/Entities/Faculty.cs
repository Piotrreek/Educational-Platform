using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class Faculty : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<Subject> _subjects = null!;
    public IReadOnlyCollection<Subject> Subjects => _subjects;
    public University University { get; private set; } = null!;
    public Guid UniversityId { get; private set; }
    private readonly List<User> _users = null!;
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