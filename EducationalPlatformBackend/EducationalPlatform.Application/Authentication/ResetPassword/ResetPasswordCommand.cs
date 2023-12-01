using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ResetPassword;

public class ResetPasswordCommand : IRequest<OneOf<Success, BadRequestResult>>
{
    public Guid UserId { get; }
    public string Token { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }
    public DateTimeOffset ResetPasswordDate { get; }

    public ResetPasswordCommand(Guid userId, string token, string password, string confirmPassword,
        DateTimeOffset? resetPasswordDate = null)
    {
        UserId = userId;
        Token = token;
        Password = password;
        ConfirmPassword = confirmPassword;
        ResetPasswordDate = resetPasswordDate ?? DateTimeOffset.Now;
    }
}