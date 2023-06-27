using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Abstractions.Services;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OneOf<Success<string>, InvalidCredentialsResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly ILogger<LoginUserCommandHandler> _logger;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtService jwtService, ILogger<LoginUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<OneOf<Success<string>, InvalidCredentialsResult>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var getUserResult = await _userRepository.GetUserByEmailAsync(request.Email);

        if (getUserResult.IsT1)
            return new InvalidCredentialsResult("Invalid credentials");

        var user = getUserResult.AsT0;

        if (!PasswordHelpers.VerifyPassword(request.Password, user.PasswordHash, Convert.FromHexString(user.Salt)))
        {
            user.AddLoginAttempt(false);
            _logger.LogInformation(@"Unsuccessful login attempt with e-mail: {email} - given password was incorrect", request.Email);
            
            return new InvalidCredentialsResult("Invalid credentials");
        }

        user.AddLoginAttempt(true);

        var token = _jwtService.Generate(user);

        return new Success<string>(token);
    }
}