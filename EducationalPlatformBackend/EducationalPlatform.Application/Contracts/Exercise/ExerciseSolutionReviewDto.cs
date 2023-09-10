namespace EducationalPlatform.Application.Contracts.Exercise;

public class ExerciseSolutionReviewDto
{
    public Guid Id { get; }
    public string? Content { get; }
    public string Author { get; }
    public DateTime CreatedOn { get; }

    public ExerciseSolutionReviewDto(Guid id, string? content, string author, DateTime createdOn)
    {
        Id = id;
        Content = content;
        Author = author;
        CreatedOn = createdOn;
    }
}