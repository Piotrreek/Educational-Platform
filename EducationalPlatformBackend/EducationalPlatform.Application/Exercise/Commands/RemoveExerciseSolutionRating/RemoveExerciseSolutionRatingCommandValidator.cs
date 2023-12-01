using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Commands.RemoveExerciseSolutionRating;

public class RemoveExerciseSolutionRatingCommandValidator : AbstractValidator<RemoveExerciseSolutionRatingCommand>
{
    public RemoveExerciseSolutionRatingCommandValidator()
    {
        RuleFor(c => c.SolutionId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveExerciseSolutionRatingCommand.SolutionId)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(RemoveExerciseSolutionRatingCommand.UserId)));
    }
}