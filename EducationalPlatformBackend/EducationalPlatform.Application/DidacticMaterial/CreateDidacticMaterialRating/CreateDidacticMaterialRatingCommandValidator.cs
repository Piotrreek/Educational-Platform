using EducationalPlatform.Application.Constants;
using EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRate;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRating;

public class CreateDidacticMaterialRatingCommandValidator : AbstractValidator<CreateDidacticMaterialRatingCommand>
{
    public CreateDidacticMaterialRatingCommandValidator()
    {
        RuleFor(c => c.Rating)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialRatingCommand.Rating)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialRatingCommand.UserId)));

        RuleFor(c => c.DidacticMaterialId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(
                    nameof(CreateDidacticMaterialRatingCommand.DidacticMaterialId)));
    }
}