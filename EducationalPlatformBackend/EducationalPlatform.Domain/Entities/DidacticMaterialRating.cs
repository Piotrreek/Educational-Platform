using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class DidacticMaterialRating : Entity
{
    public decimal Rating { get; private set; }
    public User User { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public DidacticMaterial DidacticMaterial { get; private set; } = null!;
    public Guid DidacticMaterialId { get; private set; }

    internal DidacticMaterialRating(decimal rating, Guid userId, Guid didacticMaterialId)
    {
        Rating = rating;
        UserId = userId;
        DidacticMaterialId = didacticMaterialId;
    }

    // For EF
    private DidacticMaterialRating()
    {
    }
}