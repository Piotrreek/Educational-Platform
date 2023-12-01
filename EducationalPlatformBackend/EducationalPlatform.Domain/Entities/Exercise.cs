using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class Exercise : EntityWithRatings<ExerciseRating>
{
    public string Name { get; private set; } = null!;
    public string FileName { get; private set; } = null!;
    public string? Description { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    private readonly List<ExerciseComment> _comments = new();
    public IReadOnlyCollection<ExerciseComment> Comments => _comments;
    private readonly List<ExerciseSolution> _solutions = new();
    public IReadOnlyCollection<ExerciseSolution> Solutions => _solutions;

    private Exercise(string name, string fileName, Guid authorId, string? description = null)
    {
        Name = name;
        FileName = fileName;
        AuthorId = authorId;
        Description = description;
    }

    public static Exercise Create(string name, string fileName, Guid authorId, string? description = null) =>
        new(name, fileName, authorId, description);

    public OneOf<Success<(IReadOnlyCollection<ExerciseSolution>, ExerciseSolution)>, BadRequestResult> AddSolution(
        string fileName,
        User author)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return new BadRequestResult("File name cannot be empty!");
        }

        var solution = new ExerciseSolution(fileName, author);

        _solutions.Add(solution);

        return new Success<(IReadOnlyCollection<ExerciseSolution>, ExerciseSolution)>((Solutions, solution));
    }

    public OneOf<IReadOnlyCollection<ExerciseComment>, BadRequestResult> AddComment(string comment, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            return new BadRequestResult("Comment cannot be empty!");
        }

        _comments.Add(new ExerciseComment(comment, userId));

        return _comments;
    }

    // For EF
    private Exercise()
    {
    }
}