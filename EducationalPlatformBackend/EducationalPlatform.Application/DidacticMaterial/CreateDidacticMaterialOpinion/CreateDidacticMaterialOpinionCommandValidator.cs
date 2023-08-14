using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialOpinion;

public class CreateDidacticMaterialOpinionCommandValidator : AbstractValidator<CreateDidacticMaterialOpinionCommand>
{
    public CreateDidacticMaterialOpinionCommandValidator()
    {
        RuleFor(c => c.DidacticMaterialId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialOpinionCommand
                    .DidacticMaterialId)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialOpinionCommand.UserId)));

        RuleFor(c => c.Opinion)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialOpinionCommand.Opinion)));
    }
}