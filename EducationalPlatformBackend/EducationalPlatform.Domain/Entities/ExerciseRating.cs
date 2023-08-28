using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class ExerciseRating : RatingEntity, IRatingEntity<ExerciseRating>
{
    public Exercise Exercise { get; private set; } = null!;
    public Guid ExerciseId { get; private set; }

    private ExerciseRating(decimal rating, Guid userId, Guid exerciseId) : base(rating, userId)
    {
        ExerciseId = exerciseId;
    }

    public static ExerciseRating Create(decimal rating, Guid userId, Guid id)
    {
        return new ExerciseRating(rating, userId, id);
    }

    // For EF
    private ExerciseRating()
    {
    }
}