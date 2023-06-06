namespace EducationalPlatform.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset ModifiedOn { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}