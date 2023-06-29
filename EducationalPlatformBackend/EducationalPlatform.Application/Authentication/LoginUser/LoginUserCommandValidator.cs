using EducationalPlatform.Application.Constants;
using FluentValidation;

namespace EducationalPlatform.Application.Authentication.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(luc => luc.Email)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(LoginUserCommand.Email)))
            .Matches(Regex.EmailRegex)
            .WithMessage(ValidationErrorMessages.EmailFormatMessage);

        RuleFor(luc => luc.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FieldNotEmptyMessage(nameof(LoginUserCommand.Password)));
    }
}