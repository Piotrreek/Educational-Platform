using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Exercise.Queries.GetExercise;

public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(GetExerciseQuery.Id)));
    }
}