using FluentValidation;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

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
            .Matches(
                @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$")
            .WithMessage(ValidationErrorMessages.EmailFormatMessage);

        RuleFor(ruc => ruc.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.Password)))
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessages.PasswordLengthMessage)
            .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
            .WithMessage(ValidationErrorMessages.PasswordFormatMessage);

        RuleFor(ruc => ruc.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.ConfirmPassword)))
            .Equal(ruc => ruc.Password)
            .WithMessage(ValidationErrorMessages.ValuesEqualMessage(nameof(RegisterUserCommand.ConfirmPassword),
                nameof(RegisterUserCommand.Password)));

        RuleFor(ruc => ruc.PhoneNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.PhoneNumber)))
            .Matches(@"(?<!\w)(\(?(\+|00)?48\)?)?[ -]?\d{3}[ -]?\d{3}[ -]?\d{3}(?!\w)")
            .WithMessage(ValidationErrorMessages.PhoneNumberFormatMessage);

        RuleFor(ruc => ruc.RoleName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(RegisterUserCommand.RoleName)));
    }
}