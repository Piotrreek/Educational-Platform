using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(r => r.Token)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ResetPasswordCommand.Token)));

        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ResetPasswordCommand.UserId)));

        RuleFor(r => r.ResetPasswordDate)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ResetPasswordCommand.ResetPasswordDate)));

        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ResetPasswordCommand.Password)))
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessages.PasswordLengthMessage)
            .Matches(Regex.PasswordRegex)
            .WithMessage(ValidationErrorMessages.PasswordFormatMessage);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(ResetPasswordCommand.ConfirmPassword)))
            .Equal(ruc => ruc.Password)
            .WithMessage(ValidationErrorMessages.ValuesEqualMessage(nameof(ResetPasswordCommand.ConfirmPassword),
                nameof(ResetPasswordCommand.Password)));
    }
}