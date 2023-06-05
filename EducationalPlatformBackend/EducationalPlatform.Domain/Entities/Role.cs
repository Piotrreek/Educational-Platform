using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class Role : Entity
{
    public string Name { get; } = null!;

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name can not be empty!");

        Name = name;
    }

    private Role()
    {
    }
}