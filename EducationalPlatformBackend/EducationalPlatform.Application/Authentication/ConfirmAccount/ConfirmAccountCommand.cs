using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ConfirmAccount;

public class ConfirmAccountCommand : IRequest<OneOf<Success, BadRequestResult>>
{
    public Guid UserId { get; }
    public string Token { get; }
    public DateTimeOffset ConfirmationDate { get; }

    public ConfirmAccountCommand(Guid userId, string token, DateTimeOffset? confirmationDate = null)
    {
        UserId = userId;
        Token = token;
        ConfirmationDate = confirmationDate ?? DateTimeOffset.Now;
    }
}