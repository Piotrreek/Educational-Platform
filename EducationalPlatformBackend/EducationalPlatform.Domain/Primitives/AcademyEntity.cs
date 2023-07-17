using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Primitives;

public abstract class AcademyEntity : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    protected AcademyEntity(string name)
    {
        Name = name;
    }

    protected AcademyEntity()
    {
    }
}