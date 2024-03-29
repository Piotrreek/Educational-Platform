using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterial;

public class GetDidacticMaterialQueryValidator : AbstractValidator<GetDidacticMaterialQuery>
{
    public GetDidacticMaterialQueryValidator()
    {
        RuleFor(c => c.DidacticMaterialId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(GetDidacticMaterialQuery.DidacticMaterialId)));
    }
}