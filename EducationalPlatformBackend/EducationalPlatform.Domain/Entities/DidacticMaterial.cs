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
    public DidacticMaterialType DidacticMaterialType { get; private set; }
    public string? Content { get; private set; }
    public UniversityCourse UniversityCourse { get; private set; } = null!;
    public Guid UniversityCourseId { get; private set; }
    public User Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    private readonly List<DidacticMaterialOpinion> _opinions = new();
    public IReadOnlyCollection<DidacticMaterialOpinion> Opinions => _opinions;
    private readonly List<DidacticMaterialRating> _ratings = new();
    public IReadOnlyCollection<DidacticMaterialRating> Ratings => _ratings;

    public decimal AverageRating => Ratings.Count > 0 ? (decimal)Ratings.Sum(s => s.Rating) / Ratings.Count : 0;

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

    public OneOf<Success<decimal>, BadRequestResult> AddNewRating(int rating, Guid userId)
    {
        if (rating is < 1 or > 5)
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.BadRatingValue);
        }

        if (_ratings.Any(r => r.UserId == userId))
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.CannotSetRatingTwiceByOneUser);
        }

        _ratings.Add(new DidacticMaterialRating(rating, userId, Id));

        return new Success<decimal>(AverageRating);
    }

    public decimal RemoveRatingForUser(Guid userId)
    {
        if (TryGetDidacticMaterialRating(userId, out var rating))
            _ratings.Remove(rating!);

        return AverageRating;
    }

    public void AddOpinion(string opinion, Guid userId)
    {
        _opinions.Add(new DidacticMaterialOpinion(opinion, userId));
    }

    private bool TryGetDidacticMaterialRating(Guid userId, out DidacticMaterialRating? rating)
    {
        rating = _ratings.SingleOrDefault(r => r.UserId == userId);

        return rating is not null;
    }

    // For EF
    private DidacticMaterial()
    {
    }
}