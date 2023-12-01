using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExerciseSolution;

public class CreateExerciseSolutionCommandValidator : AbstractValidator<CreateExerciseSolutionCommand>
{
    public CreateExerciseSolutionCommandValidator()
    {
        RuleFor(c => c.SolutionFile)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionCommand.SolutionFile)))
            .Must(c => c.ContentType == "application/pdf")
            .WithMessage(ValidationErrorMessages.PdfFileError);

        RuleFor(c => c.AuthorId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionCommand.AuthorId)));

        RuleFor(c => c.ExerciseId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionCommand.ExerciseId)));
    }
}