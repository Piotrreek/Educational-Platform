using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.LoginUser;

public record LoginUserCommand
    (string Email, string Password, DateTimeOffset LoginDateTimeOffset) : IRequest<OneOf<Success<LoginUserResponseDto>, InvalidCredentialsResult>>;