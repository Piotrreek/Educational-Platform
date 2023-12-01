using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.SendResetPasswordLink;

internal sealed class SendResetPasswordLinkCommandHandler : IRequestHandler<SendResetPasswordLinkCommand, OneOf<Success, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;

    public SendResetPasswordLinkCommandHandler(IUserRepository userRepository, IPublisher publisher)
    {
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task<OneOf<Success, NotFound>> Handle(SendResetPasswordLinkCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userResult.IsT1)
            return new NotFound();

        var user = userResult.AsT0;
        var token = TokenUtils.GenerateToken(64);

        user.ChangeExpirationDateOfUserTokensOfGivenType(TokenType.ResetPasswordToken, DateTimeOffset.Now);
        user.AddUserToken(token, TokenType.ResetPasswordToken);

        await _publisher.Publish(new ResetPasswordTokenAddedToUser(request.Email, user.Id, token), cancellationToken);

        return new Success();
    }
}