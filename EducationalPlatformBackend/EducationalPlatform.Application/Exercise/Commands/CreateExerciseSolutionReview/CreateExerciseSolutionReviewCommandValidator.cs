using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExerciseSolutionReview;

public class CreateExerciseSolutionReviewCommandValidator : AbstractValidator<CreateExerciseSolutionReviewCommand>
{
    public CreateExerciseSolutionReviewCommandValidator()
    {
        RuleFor(c => c.AuthorId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionReviewCommand.AuthorId)));

        RuleFor(c => c.ExerciseSolutionId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionReviewCommand
                    .ExerciseSolutionId)));
    }
}