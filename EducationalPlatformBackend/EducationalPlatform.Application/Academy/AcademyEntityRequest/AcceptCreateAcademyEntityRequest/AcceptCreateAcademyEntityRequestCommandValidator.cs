using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.AcceptCreateAcademyEntityRequest;

public class
    AcceptCreateAcademyEntityRequestCommandValidator : AbstractValidator<AcceptCreateAcademyEntityRequestCommand>
{
    public AcceptCreateAcademyEntityRequestCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(AcceptCreateAcademyEntityRequestCommand.Id)));
    }
}