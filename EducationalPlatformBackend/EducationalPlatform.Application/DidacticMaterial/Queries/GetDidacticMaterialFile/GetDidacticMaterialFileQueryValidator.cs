using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.Queries.GetDidacticMaterialFile;

public class GetDidacticMaterialFileQueryValidator : AbstractValidator<GetDidacticMaterialFileQuery>
{
    public GetDidacticMaterialFileQueryValidator()
    {
        RuleFor(c => c.DidacticMaterialId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(GetDidacticMaterialFileQuery.DidacticMaterialId)));
    }
}