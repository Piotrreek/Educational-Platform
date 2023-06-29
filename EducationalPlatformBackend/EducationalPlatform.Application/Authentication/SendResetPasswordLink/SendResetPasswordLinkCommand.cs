using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.SendResetPasswordLink;

public record SendResetPasswordLinkCommand(string Email) : IRequest<OneOf<Success, NotFound>>;