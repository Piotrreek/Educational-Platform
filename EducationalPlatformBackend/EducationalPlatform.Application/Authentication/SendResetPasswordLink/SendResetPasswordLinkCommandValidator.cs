using EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;
using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.SendResetPasswordLink;

public class SendResetPasswordLinkCommandValidator : AbstractValidator<SendResetPasswordLinkCommand>
{
    public SendResetPasswordLinkCommandValidator()
    {
        RuleFor(s => s.Email)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(SendAccountConfirmationLinkCommand.Email)))
            .Matches(Regex.EmailRegex)
            .WithMessage(ValidationErrorMessages.EmailFormatMessage);
        
    }
}