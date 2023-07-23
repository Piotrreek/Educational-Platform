using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterial;

public class CreateDidacticMaterialCommandValidator : AbstractValidator<CreateDidacticMaterialCommand>
{
    public CreateDidacticMaterialCommandValidator()
    {
        RuleFor(c => c.DidacticMaterialType)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialCommand
                    .DidacticMaterialType)));

        RuleFor(c => c.UniversityCourseId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialCommand.UniversityCourseId)));

        RuleFor(c => c.AuthorId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateDidacticMaterialCommand.AuthorId)));
    }
}