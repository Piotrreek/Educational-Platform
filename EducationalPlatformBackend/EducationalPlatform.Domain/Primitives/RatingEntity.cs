using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Primitives;

public abstract class RatingEntity : Entity
{
    public decimal Rating { get; private set; }
    public User User { get; private set; } = null!;
    public Guid UserId { get; private set; }

    protected RatingEntity(decimal rating, Guid userId)
    {
        Rating = rating;
        UserId = userId;
    }
    
    // For EF
    protected RatingEntity()
    {
    }
}