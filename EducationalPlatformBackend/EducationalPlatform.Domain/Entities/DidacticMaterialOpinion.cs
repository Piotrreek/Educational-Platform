using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class DidacticMaterialOpinion : Entity
{
    public string Opinion { get; private set; } = null!;
    public DidacticMaterial DidacticMaterial { get; private set; } = null!;
    public Guid DidacticMaterialId { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
}