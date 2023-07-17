using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Primitives;

public abstract class AcademyEntity : Entity
{
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;
}