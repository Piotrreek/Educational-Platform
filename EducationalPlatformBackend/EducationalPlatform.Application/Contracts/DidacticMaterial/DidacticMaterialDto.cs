namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class DidacticMaterialDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string Author { get; }
    public decimal AverageRating { get; }

    public DidacticMaterialDto(Guid id, string name, string author, decimal averageRating)
    {
        Id = id;
        Name = name;
        Author = author;
        AverageRating = averageRating;
    }
}