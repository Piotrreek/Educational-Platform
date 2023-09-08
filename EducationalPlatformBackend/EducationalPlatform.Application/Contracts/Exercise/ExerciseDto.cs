namespace EducationalPlatform.Application.Contracts.Exercise;

public class ExerciseDto : BaseExerciseDto
{
    public string? Description { get; }
    public IEnumerable<ExerciseSolutionDto> Solutions { get; }
    public IEnumerable<OpinionDto> Comments { get; }
    public IEnumerable<decimal> LastRatings { get; }
    public bool Rateable { get; }
    public decimal UsersRate { get; }

    public ExerciseDto(Guid id, string name, string? description, string author, IEnumerable<ExerciseSolutionDto> solutions,
        decimal averageRating, IEnumerable<decimal> lastRatings, decimal usersRate, bool rateable,
        IEnumerable<OpinionDto> comments) : base(id, name, author, averageRating)
    {
        Description = description;
        Solutions = solutions;
        LastRatings = lastRatings;
        UsersRate = usersRate;
        Rateable = rateable;
        Comments = comments;
    }
}