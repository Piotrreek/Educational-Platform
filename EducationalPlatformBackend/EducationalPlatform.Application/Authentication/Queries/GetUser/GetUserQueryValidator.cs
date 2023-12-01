using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(GetUserQuery.UserId)));
    }
}