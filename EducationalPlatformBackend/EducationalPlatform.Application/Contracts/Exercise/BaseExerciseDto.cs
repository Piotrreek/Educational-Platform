namespace EducationalPlatform.Application.Contracts.Exercise;

public class BaseExerciseDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string Author { get; }
    public decimal AverageRating { get; }

    public BaseExerciseDto(Guid id, string name, string author, decimal averageRating)
    {
        Id = id;
        Name = name;
        Author = author;
        AverageRating = averageRating;
    }
}