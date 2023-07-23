using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class CreateDidacticMaterialRequestDto
{
    public string? Name { get; set; }
    public string DidacticMaterialType { get; set; }
    public Guid UniversityCourseId { get; set; }
    public string[]? Keywords { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public IFormFile? File { get; set; }

    public CreateDidacticMaterialRequestDto()
    {
        DidacticMaterialType = string.Empty;
    }
}