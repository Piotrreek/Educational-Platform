using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class DidacticMaterial : Entity
{
    public string Name { get; private set; } = null!;
    public string? Keywords { get; private set; }
    public string? Description { get; private set; }
    public int RatingsCount { get; private set; }
    public decimal AverageRating { get; private set; }
    public DidacticMaterialType DidacticMaterialType { get; private set; }
    public string? Content { get; private set; }
    public UniversityCourse UniversityCourse { get; private set; } = null!;
    public Guid UniversityCourseId { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    private readonly List<DidacticMaterialOpinion> _opinions = new();
    public IReadOnlyCollection<DidacticMaterialOpinion> Opinions => _opinions;

    public DidacticMaterial(string name, Guid universityCourseId, Guid authorId,
        DidacticMaterialType didacticMaterialType, string[]? keywords = null, string? description = null)
    {
        Name = name;
        UniversityCourseId = universityCourseId;
        AuthorId = authorId;
        DidacticMaterialType = didacticMaterialType;
        Keywords = keywords?.Any() ?? false ? string.Join(';', keywords) : null;
        Description = description;
    }

    public OneOf<Success, BadRequestResult> SetContent(string content)
    {
        if (DidacticMaterialType == DidacticMaterialType.File)
            return new BadRequestResult(DidacticMaterialErrorMessages.CannotSetContent);

        Content = content;

        return new Success();
    }

    public OneOf<Success<decimal>, BadRequestResult> AddNewRating(int newRating)
    {
        if (newRating is < 0 or > 5)
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.BadRatingValue);
        }

        var currentRatingsSum = RatingsCount * AverageRating + newRating;
        RatingsCount += 1;
        AverageRating = currentRatingsSum / RatingsCount;

        return new Success<decimal>(AverageRating);
    }

    // For EF
    private DidacticMaterial()
    {
    }
}