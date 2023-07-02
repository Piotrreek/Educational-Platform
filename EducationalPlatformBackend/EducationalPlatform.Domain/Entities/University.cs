using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class University : Entity
{
    private readonly List<Faculty> _faculties = null!;
    public IReadOnlyCollection<Faculty> Faculties => _faculties;
}