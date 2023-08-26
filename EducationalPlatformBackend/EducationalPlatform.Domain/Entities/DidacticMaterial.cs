using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class DidacticMaterial : EntityWithRatings<DidacticMaterialRating>
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
    public UniversitySubject UniversitySubject => UniversityCourse.UniversitySubject;
    public Faculty Faculty => UniversitySubject.Faculty;
    public University University => Faculty.University;

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
    
    public OneOf<IEnumerable<DidacticMaterialOpinion>, BadRequestResult> AddOpinion(string opinion,
        Guid userId)
    {
        if (string.IsNullOrWhiteSpace(opinion))
            return new BadRequestResult(DidacticMaterialErrorMessages.OpinionCannotBeEmpty);

        _opinions.Add(new DidacticMaterialOpinion(opinion, userId));

        return _opinions;
    }
    
    // For EF
    private DidacticMaterial()
    {
    }
}