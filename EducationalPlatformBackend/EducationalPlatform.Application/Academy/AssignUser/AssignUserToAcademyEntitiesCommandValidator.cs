using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.AssignUser;

public class AssignUserToAcademyEntitiesCommandValidator : AbstractValidator<AssignUserToAcademyEntitiesCommand>
{
    public AssignUserToAcademyEntitiesCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(AssignUserToAcademyEntitiesCommand.UserId)));
    }
}