using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class DidacticMaterialRating : RatingEntity, IRatingEntity<DidacticMaterialRating>
{
    public DidacticMaterial DidacticMaterial { get; private set; } = null!;
    public Guid DidacticMaterialId { get; private set; }

    private DidacticMaterialRating(decimal rating, Guid userId, Guid didacticMaterialId) : base(rating, userId)
    {
        DidacticMaterialId = didacticMaterialId;
    }

    public static DidacticMaterialRating Create(decimal rating, Guid userId, Guid id)
    {
        return new DidacticMaterialRating(rating, userId, id);
    }

    // For EF
    private DidacticMaterialRating()
    {
    }
}