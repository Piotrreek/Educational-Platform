namespace EducationalPlatform.Application.Contracts.Exercise;

public class ExerciseSolutionReviewDto
{
    public Guid Id { get; }
    public string? Content { get; }
    public string Author { get; }
    public DateTime CreatedOn { get; }
    public bool HasFile { get; }

    public ExerciseSolutionReviewDto(Guid id, string? content, string author, DateTime createdOn, bool hasFile)
    {
        Id = id;
        Content = content;
        Author = author;
        CreatedOn = createdOn;
        HasFile = hasFile;
    }
}