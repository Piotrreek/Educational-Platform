namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class AverageRatingDto
{
    public decimal AverageRating { get; }

    public AverageRatingDto(decimal averageRating)
    {
        AverageRating = averageRating;
    }
}