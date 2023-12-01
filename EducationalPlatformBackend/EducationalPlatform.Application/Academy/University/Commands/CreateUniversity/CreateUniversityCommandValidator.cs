using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.University.Commands.CreateUniversity;

public class CreateUniversityCommandValidator : AbstractValidator<CreateUniversityCommand>
{
    public CreateUniversityCommandValidator()
    {
        RuleFor(c => c.UniversityName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversityCommand.UniversityName)));
    }
}