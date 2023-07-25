namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class CreateDidacticMaterialRatingRequestDto
{
    public int Rating { get; set; }
    public Guid DidacticMaterialId { get; set; }
}