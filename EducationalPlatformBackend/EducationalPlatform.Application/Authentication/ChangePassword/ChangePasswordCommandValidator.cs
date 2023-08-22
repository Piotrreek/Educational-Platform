using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ChangePasswordCommand.NewPassword)))
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessages.PasswordLengthMessage)
            .Matches(Regex.PasswordRegex)
            .WithMessage(ValidationErrorMessages.PasswordFormatMessage);

        RuleFor(c => c.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ChangePasswordCommand.ConfirmNewPassword)))
            .Equal(ruc => ruc.NewPassword)
            .WithMessage(ValidationErrorMessages.ValuesEqualMessage(nameof(ChangePasswordCommand.ConfirmNewPassword),
                nameof(ChangePasswordCommand.NewPassword)));

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ChangePasswordCommand.UserId)));
    }
}