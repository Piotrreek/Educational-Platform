using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.RemoveDidacticMaterialRating;

public class RemoveDidacticMaterialRatingCommandValidator : AbstractValidator<RemoveDidacticMaterialRatingCommand>
{
    public RemoveDidacticMaterialRatingCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveDidacticMaterialRatingCommand.UserId)));

        RuleFor(c => c.DidacticMaterialId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveDidacticMaterialRatingCommand
                    .DidacticMaterialId)));
    }
}