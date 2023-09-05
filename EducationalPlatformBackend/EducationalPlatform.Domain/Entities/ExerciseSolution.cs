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

    internal ExerciseSolution(string fileName, User author)
    {
        FileName = fileName;
        Author = author;
    }

    public Guid AddReview(Guid authorId, string? content, string? fileName)
    {
        var review = new ExerciseSolutionReview(authorId, fileName, content);

        _reviews.Add(review);

        return review.Id;
    }

    // For EF
    private ExerciseSolution()
    {
    }
}