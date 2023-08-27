using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class ExerciseSolutionReview : Entity
{
    public string? FileName { get; set; }
    public string? Content { get; set; }
    public ExerciseSolution ExerciseSolution { get; private set; } = null!;
    public Guid ExerciseSolutionId { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }

    internal ExerciseSolutionReview(Guid authorId, string? fileName = null,
        string? content = null)
    {
        AuthorId = authorId;
        FileName = fileName;
        Content = content;
    }
}