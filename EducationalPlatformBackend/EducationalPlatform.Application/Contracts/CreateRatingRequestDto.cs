namespace EducationalPlatform.Application.Contracts;

public class CreateRatingRequestDto
{
    public decimal Rating { get; set; }
    public Guid EntityId { get; set; }
}