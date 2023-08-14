namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class RatingDto
{
    public decimal AverageRating { get; }
    public IEnumerable<decimal> LastRatings { get; }

    public RatingDto(decimal averageRating, IEnumerable<decimal> lastRatings)
    {
        AverageRating = averageRating;
        LastRatings = lastRatings;
    }
}