namespace EducationalPlatform.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTimeOffset CreatedOn { get; protected set; }
    public DateTimeOffset? ModifiedOn { get; protected set; }

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
}