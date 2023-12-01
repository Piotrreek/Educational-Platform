using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.Commands.SendAccountConfirmationLink;

public record SendAccountConfirmationLinkCommand(string Email) : IRequest<OneOf<Success, NotFound, BadRequestResult>>;