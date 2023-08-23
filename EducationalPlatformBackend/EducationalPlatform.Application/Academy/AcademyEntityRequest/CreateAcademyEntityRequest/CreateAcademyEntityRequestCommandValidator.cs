using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.CreateAcademyEntityRequest;

public class CreateAcademyEntityRequestCommandValidator : AbstractValidator<CreateAcademyEntityRequestCommand>
{
    public CreateAcademyEntityRequestCommandValidator()
    {
        RuleFor(c => c.EntityType)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateAcademyEntityRequestCommand.EntityType)))
            .Must(c => CreateAcademyEntityRequestCommandHandler.ValidTypeNames.Contains(c))
            .WithMessage(ValidationErrorMessages.WrongType);

        RuleFor(c => c.EntityName)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateAcademyEntityRequestCommand.EntityName)));

        RuleFor(c => c.RequesterId)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessages.FieldNotEmptyMessage(nameof(CreateAcademyEntityRequestCommand.RequesterId)));
    }
}