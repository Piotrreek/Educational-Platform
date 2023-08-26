using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class ExerciseComment : Entity
{
    public string Comment { get; private set; } = null!;
    public Exercise Exercise { get; private set; } = null!;
    public Guid ExerciseId { get; private set; }
    public User Author { get; private set; } = null;
    public Guid AuthorId { get; private set; }

    internal ExerciseComment(string comment, Guid authorId)
    {
        Comment = comment;
        AuthorId = authorId;
    }

    // For EF
    private ExerciseComment()
    {
    }
}