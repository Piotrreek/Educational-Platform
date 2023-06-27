using EducationalPlatform.Domain;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ConfirmAccount;

public class ConfirmAccountCommandHandler : IRequestHandler<ConfirmAccountCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;

    public ConfirmAccountCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(ConfirmAccountCommand request,
        CancellationToken cancellationToken)
    {
        var getUserResult = await _userRepository.GetUserByIdAsync(request.UserId);
        if (getUserResult.IsT1)
            return new BadRequestResult(ErrorMessages.BadAccountConfirmationLinkMessage);

        var user = getUserResult.AsT0;
        var confirmAccountResult = user.ConfirmAccount(request.Token, request.ConfirmationDate);

        return confirmAccountResult.Match<OneOf<Success, BadRequestResult>>(
            success => success,
            badRequest => badRequest
        );
    }
}