using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ResetPassword;

public class ResetPasswordCommand : IRequest<OneOf<Success, BadRequestResult>>
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public DateTimeOffset ResetPasswordDate { get; set; }

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