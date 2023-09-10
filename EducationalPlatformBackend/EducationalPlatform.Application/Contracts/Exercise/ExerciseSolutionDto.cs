namespace EducationalPlatform.Application.Contracts.Exercise;

public class ExerciseSolutionDto
{
    public Guid Id { get; }
    public string Author { get; }
    public DateTime CreatedOn { get; }
    public decimal AverageRating { get; }
    public decimal UsersRating { get; }
    public IEnumerable<ExerciseSolutionReviewDto> Reviews { get; }

    public ExerciseSolutionDto(Guid id, string author, DateTime createdOn, decimal averageRating, decimal usersRating,
        IEnumerable<ExerciseSolutionReviewDto> reviews)
    {
        Id = id;
        Author = author;
        CreatedOn = createdOn;
        AverageRating = averageRating;
        UsersRating = usersRating;
        Reviews = reviews;
    }
}