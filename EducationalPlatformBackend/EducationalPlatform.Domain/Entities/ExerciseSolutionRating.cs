using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class ExerciseSolutionRating : RatingEntity, IRatingEntity<ExerciseSolutionRating>
{
    public ExerciseSolution ExerciseSolution { get; private set; } = null!;
    public Guid ExerciseSolutionId { get; private set; }

    private ExerciseSolutionRating(decimal rating, Guid userId, Guid exerciseSolutionId) : base(rating, userId)
    {
        ExerciseSolutionId = exerciseSolutionId;
    }

    public static ExerciseSolutionRating Create(decimal rating, Guid userId, Guid id)
    {
        return new ExerciseSolutionRating(rating, userId, id);
    }

    // For EF
    private ExerciseSolutionRating()
    {
    }
}