using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);

        if (!userResult.TryPickT0(out var user, out _))
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        if (!PasswordHelpers.VerifyPassword(request.OldPassword, user.PasswordHash, Convert.FromHexString(user.Salt)))
        {
            return new BadRequestResult(UserErrorMessages.WrongPassword);
        }

        var newPasswordHash = PasswordHelpers.HashPassword(request.NewPassword, out var newSalt);

        user.ChangePassword(newPasswordHash, Convert.ToHexString(newSalt));

        return new Success();
    }
}