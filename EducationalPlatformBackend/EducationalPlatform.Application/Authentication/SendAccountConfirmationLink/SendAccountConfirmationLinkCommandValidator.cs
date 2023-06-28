using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;

public class SendAccountConfirmationLinkCommandValidator : AbstractValidator<SendAccountConfirmationLinkCommand>
{
    public SendAccountConfirmationLinkCommandValidator()
    {
        RuleFor(ruc => ruc.Email)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(SendAccountConfirmationLinkCommand.Email)))
            .Matches(ConstantValues.EmailRegex)
            .WithMessage(ValidationErrorMessages.EmailFormatMessage);
    }
}