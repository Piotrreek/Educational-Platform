using EducationalPlatform.Domain;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ConfirmAccount;

internal sealed class ConfirmAccountCommandHandler : IRequestHandler<ConfirmAccountCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ConfirmAccountCommandHandler> _logger;

    public ConfirmAccountCommandHandler(IUserRepository userRepository, ILogger<ConfirmAccountCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(ConfirmAccountCommand request,
        CancellationToken cancellationToken)
    {
        var getUserResult = await _userRepository.GetUserByIdAsync(request.UserId);
        if (getUserResult.IsT1)
            return new BadRequestResult(GeneralErrorMessages.BadAccountConfirmationLinkMessage);

        var user = getUserResult.AsT0;
        var confirmAccountResult = user.ConfirmAccount(request.Token, request.ConfirmationDate);

        return confirmAccountResult.Match<OneOf<Success, BadRequestResult>>(
            success =>
            {
                _logger.LogInformation(@"Account with email {email} was confirmed", user.Email);
                return success;
            },
            badRequest =>
            {
                _logger.LogInformation(@"Unsuccessful account confirmation with email {email}", user.Email);
                return badRequest;
            });
    }
}