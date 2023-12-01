using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(ruc => ruc.Username)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.Username)));

        RuleFor(ruc => ruc.Email)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.Email)))
            .Matches(Regex.EmailRegex)
            .WithMessage(ValidationErrorMessages.EmailFormatMessage);

        RuleFor(ruc => ruc.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.Password)))
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessages.PasswordLengthMessage)
            .Matches(Regex.PasswordRegex)
            .WithMessage(ValidationErrorMessages.PasswordFormatMessage);

        RuleFor(ruc => ruc.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.ConfirmPassword)))
            .Equal(ruc => ruc.Password)
            .WithMessage(ValidationErrorMessages.ValuesEqualMessage(nameof(RegisterUserCommand.ConfirmPassword),
                nameof(RegisterUserCommand.Password)));

        RuleFor(ruc => ruc.RequestedRoleName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.RequestedRoleName)));
    }
}