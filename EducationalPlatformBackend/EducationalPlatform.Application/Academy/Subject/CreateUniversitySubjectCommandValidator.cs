using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.Subject;

public class CreateUniversitySubjectCommandValidator : AbstractValidator<CreateUniversitySubjectCommand>
{
    public CreateUniversitySubjectCommandValidator()
    {
        RuleFor(c => c.FacultyId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversitySubjectCommand.FacultyId)));

        RuleFor(c => c.SubjectDegree)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversitySubjectCommand.SubjectDegree)));

        RuleFor(c => c.SubjectName)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversitySubjectCommand.SubjectName)));
    }
}