namespace EducationalPlatform.Domain.Primitives;

public interface IRatingEntity<out T> where T : RatingEntity
{
    static abstract T Create(decimal rating, Guid userId, Guid id);
}