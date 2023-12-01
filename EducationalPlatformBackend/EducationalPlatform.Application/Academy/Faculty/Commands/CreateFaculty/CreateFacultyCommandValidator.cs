using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.Faculty.Commands.CreateFaculty;

public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
{
    public CreateFacultyCommandValidator()
    {
        RuleFor(c => c.FacultyName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateFacultyCommand.FacultyName)));

        RuleFor(c => c.UniversityId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateFacultyCommand.UniversityId)));
    }
}