using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ResetPassword;

public class
    ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;

    public ResetPasswordCommandHandler(IUserRepository userRepository, ILogger<ResetPasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);
        if (userResult.IsT1)
            return new BadRequestResult(GeneralErrorMessages.BadResetPasswordLinkMessage);

        var user = userResult.AsT0;
        var newPasswordHash = PasswordHelpers.HashPassword(request.Password, out var newSalt);
        var changePasswordResult = user.ResetPassword(newPasswordHash, Convert.ToHexString(newSalt), request.Token,
            request.ResetPasswordDate);

        return changePasswordResult.Match<OneOf<Success, BadRequestResult>>(
            success =>
            {
                _logger.LogInformation(@"Account with email {email} has changed password successfully", user.Email);
                return success;
            },
            badRequest =>
            {
                _logger.LogInformation(@"Unsuccessful password change of account with email {email}", user.Email);
                return badRequest;
            });
    }
}