using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class Subject : Entity
{
    public Faculty Faculty { get; private set; } = null!;
    public Guid FacultyId { get; private set; }
}