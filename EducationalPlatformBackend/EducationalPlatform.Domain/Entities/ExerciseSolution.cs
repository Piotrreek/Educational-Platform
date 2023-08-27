using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class ExerciseSolution : EntityWithRatings<ExerciseSolutionRating>
{
    public string FileName { get; private set; } = null!;
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;
    public Guid ExerciseId { get; private set; }
    private readonly List<ExerciseSolutionReview> _reviews = new();
    public IReadOnlyCollection<ExerciseSolutionReview> Reviews => _reviews;

    internal ExerciseSolution(string fileName, Guid authorId)
    {
        FileName = fileName;
        AuthorId = authorId;
    }

    // For EF
    private ExerciseSolution()
    {
    }
}