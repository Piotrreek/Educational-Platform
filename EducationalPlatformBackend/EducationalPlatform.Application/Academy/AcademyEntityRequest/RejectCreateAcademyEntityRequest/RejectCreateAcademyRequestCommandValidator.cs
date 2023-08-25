using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.RejectCreateAcademyEntityRequest;

public class RejectCreateAcademyRequestCommandValidator : AbstractValidator<RejectCreateAcademyRequestCommand>
{
    public RejectCreateAcademyRequestCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RejectCreateAcademyRequestCommand.Id)));
    }
}