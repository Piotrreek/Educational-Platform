using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Primitives;

public abstract class AcademyEntity : Entity
{
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;
    
}