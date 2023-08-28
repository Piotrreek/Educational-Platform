using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.CreateExerciseRating;

public class CreateExerciseRatingCommandValidator : AbstractValidator<CreateExerciseRatingCommand>
{
    public CreateExerciseRatingCommandValidator()
    {
        RuleFor(c => c.Rating)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseRatingCommand.Rating)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateExerciseRatingCommand.UserId)));

        RuleFor(c => c.ExerciseId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(
                    nameof(CreateExerciseRatingCommand.ExerciseId)));
    }
}