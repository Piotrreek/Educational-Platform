namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class DidacticMaterialOpinionDto
{
    public DateTime CreatedOn { get; }
    public string Author { get; }
    public string Opinion { get; }

    public DidacticMaterialOpinionDto(DateTime createdOn, string author, string opinion)
    {
        CreatedOn = createdOn;
        Author = author;
        Opinion = opinion;
    }
}