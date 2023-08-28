using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.CreateExerciseComment;

public class CreateExerciseCommentCommandValidator : AbstractValidator<CreateExerciseCommentCommand>
{
    public CreateExerciseCommentCommandValidator()
    {
        RuleFor(c => c.Comment)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommentCommand.Comment)));

        RuleFor(c => c.ExerciseId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommentCommand.ExerciseId)));

        RuleFor(c => c.AuthorId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseCommentCommand.AuthorId)));
    }
}