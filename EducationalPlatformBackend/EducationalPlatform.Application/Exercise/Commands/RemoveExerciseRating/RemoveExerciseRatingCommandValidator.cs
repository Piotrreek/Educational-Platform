using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Commands.RemoveExerciseRating;

public class RemoveExerciseRatingCommandValidator : AbstractValidator<RemoveExerciseRatingCommand>
{
    public RemoveExerciseRatingCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveExerciseRatingCommand.UserId)));

        RuleFor(c => c.ExerciseId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveExerciseRatingCommand.ExerciseId)));
    }
}