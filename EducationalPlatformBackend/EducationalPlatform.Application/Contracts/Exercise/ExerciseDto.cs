namespace EducationalPlatform.Application.Contracts.Exercise;

public class ExerciseDto
{
    public string Name { get; }
    public string? Description { get; }
    public string Author { get; }
    public IEnumerable<ExerciseSolutionDto> Solutions { get; }
    public decimal AverageRating { get; }
    public IEnumerable<decimal> LastRatings { get; }
    public bool Rateable { get; }
    public decimal UsersRate { get; }

    public ExerciseDto(string name, string? description, string author, IEnumerable<ExerciseSolutionDto> solutions,
        decimal averageRating, IEnumerable<decimal> lastRatings, decimal usersRate, bool rateable)
    {
        Name = name;
        Description = description;
        Author = author;
        Solutions = solutions;
        AverageRating = averageRating;
        LastRatings = lastRatings;
        UsersRate = usersRate;
        Rateable = rateable;
    }
}