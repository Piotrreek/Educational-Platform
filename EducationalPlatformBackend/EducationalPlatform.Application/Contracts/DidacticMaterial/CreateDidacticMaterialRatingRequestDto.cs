namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class CreateDidacticMaterialRatingRequestDto
{
    public decimal Rating { get; set; }
    public Guid DidacticMaterialId { get; set; }
}