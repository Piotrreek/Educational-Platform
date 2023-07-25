using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.Course;

public class CreateUniversityCourseCommandValidator : AbstractValidator<CreateUniversityCourseCommand>
{
    public CreateUniversityCourseCommandValidator()
    {
        RuleFor(c => c.CourseName)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversityCourseCommand.CourseName)));

        RuleFor(c => c.CourseSession)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversityCourseCommand.CourseSession)));

        RuleFor(c => c.SubjectId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateUniversityCourseCommand.SubjectId)));
    }
}