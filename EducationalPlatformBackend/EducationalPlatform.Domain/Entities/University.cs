using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class University : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<Faculty> _faculties = new();
    public IReadOnlyCollection<Faculty> Faculties => _faculties;
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public University(string name)
    {
        Name = name;
    }

    // For EF
    private University()
    {
    }
}