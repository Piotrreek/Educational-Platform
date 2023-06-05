namespace EducationalPlatform.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}