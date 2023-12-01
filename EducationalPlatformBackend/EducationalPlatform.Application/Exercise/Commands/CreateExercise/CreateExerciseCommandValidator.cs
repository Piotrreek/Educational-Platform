using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommand.Name)));

        RuleFor(c => c.ExerciseFile)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommand.ExerciseFile)))
            .Must(c => c.ContentType == "application/pdf")
            .WithMessage(ValidationErrorMessages.PdfFileError);

        RuleFor(c => c.AuthorId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommand.AuthorId)));
    }
}