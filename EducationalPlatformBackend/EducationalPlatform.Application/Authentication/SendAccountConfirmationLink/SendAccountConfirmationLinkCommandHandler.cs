using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;

internal sealed class SendAccountConfirmationLinkCommandHandler : IRequestHandler<SendAccountConfirmationLinkCommand,
    OneOf<Success, NotFound, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;

    public SendAccountConfirmationLinkCommandHandler(IUserRepository userRepository, IPublisher publisher)
    {
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task<OneOf<Success, NotFound, BadRequestResult>> Handle(SendAccountConfirmationLinkCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userResult.IsT1)
            return new NotFound();

        var user = userResult.AsT0;
        if (user.EmailConfirmed)
            return new BadRequestResult(GeneralErrorMessages.AccountAlreadyConfirmedErrorMessage);

        var token = TokenUtils.GenerateToken(64);

        user.ChangeExpirationDateOfUserTokensOfGivenType(TokenType.AccountConfirmationToken, DateTimeOffset.Now);
        user.AddUserToken(token, TokenType.AccountConfirmationToken);

        await _publisher.Publish(new AccountConfirmationTokenAddedToUser(user.Id, token, request.Email),
            cancellationToken);

        return new Success();
    }
}