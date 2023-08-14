namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class CreateDidacticMaterialOpinionRequestDto
{
    public Guid DidacticMaterialId { get; set; }
    public string Opinion { get; set; } = null!;
}