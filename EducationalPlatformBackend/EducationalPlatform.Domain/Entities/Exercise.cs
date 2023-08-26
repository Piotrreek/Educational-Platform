using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class Exercise : EntityWithRatings<ExerciseRating>
{
    public string Name { get; private set; } = null!;
    public string FileName { get; private set; } = null!;
    public string? Description { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
}