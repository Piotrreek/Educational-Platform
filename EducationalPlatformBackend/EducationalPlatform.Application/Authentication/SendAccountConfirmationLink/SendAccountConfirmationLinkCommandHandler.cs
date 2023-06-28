using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;

public class SendAccountConfirmationLinkCommandHandler : IRequestHandler<SendAccountConfirmationLinkCommand,
    OneOf<Success, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;

    public SendAccountConfirmationLinkCommandHandler(IUserRepository userRepository, IPublisher publisher)
    {
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task<OneOf<Success, NotFound>> Handle(SendAccountConfirmationLinkCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userResult.IsT1)
            return new NotFound();

        var user = userResult.AsT0;
        var token = TokenUtils.GenerateToken(64);
        
        user.ChangeExpirationDateOfUserTokensOfGivenType(TokenType.AccountConfirmationToken, DateTimeOffset.Now);
        user.AddUserToken(token, TokenType.AccountConfirmationToken);
        
        await _publisher.Publish(new AccountConfirmationTokenAddedToUser(user.Id, user.Email, token),
            cancellationToken);

        return new Success();
    }
}