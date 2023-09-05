namespace EducationalPlatform.Application.Contracts.Exercise;

public class SolutionRatingDto
{
    public decimal AverageRating { get; }
    public decimal UsersRating { get; }

    public SolutionRatingDto(decimal averageRating, decimal usersRating)
    {
        AverageRating = averageRating;
        UsersRating = usersRating;
    }
}