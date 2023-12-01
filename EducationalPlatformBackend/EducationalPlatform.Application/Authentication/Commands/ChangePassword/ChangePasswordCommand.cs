using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(
    string OldPassword,
    string NewPassword,
    string ConfirmNewPassword,
    Guid UserId
) : IRequest<OneOf<Success, BadRequestResult>>;