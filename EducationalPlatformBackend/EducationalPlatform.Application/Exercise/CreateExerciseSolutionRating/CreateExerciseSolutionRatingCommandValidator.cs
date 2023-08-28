using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolutionRating;

public class
    CreateExerciseSolutionRatingCommandValidator : AbstractValidator<CreateExerciseSolutionRatingCommand>
{
    public CreateExerciseSolutionRatingCommandValidator()
    {
        RuleFor(c => c.Rating)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionRatingCommand.Rating)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseSolutionRatingCommand.UserId)));

        RuleFor(c => c.SolutionId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(
                    nameof(CreateExerciseSolutionRatingCommand.SolutionId)));
    }
}