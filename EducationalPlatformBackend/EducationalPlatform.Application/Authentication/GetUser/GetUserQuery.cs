using EducationalPlatform.Application.Contracts.Authentication;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<OneOf<UserDto, NotFound>>;