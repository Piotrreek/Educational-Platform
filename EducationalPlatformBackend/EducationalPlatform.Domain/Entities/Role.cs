using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class Role : Entity
{
    public string Name { get; } = null!;

    public Role(string name, Guid? id = null, DateTimeOffset? createdOn = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name can not be empty!");

        Name = name;
        if (createdOn is not null)
            CreatedOn = (DateTimeOffset)createdOn;
    }

    private Role()
    {
    }
}