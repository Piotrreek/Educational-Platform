using EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;
using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.Results;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand,
    OneOf<NoContentResult, EmailInUseResult, NotAppropriateRoleResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IPublisher _publisher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        ILogger<RegisterUserCommandHandler> logger, IPublisher publisher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task<OneOf<NoContentResult, EmailInUseResult, NotAppropriateRoleResult>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailUniqueAsync(request.Email))
            return new EmailInUseResult();

        var getRoleNameResult = await GetRoleName(request.UserId, request.RequestedRoleName);
        if (getRoleNameResult.TryPickT0(out var forbiddenRoleResult, out _))
            return new NotAppropriateRoleResult(forbiddenRoleResult.Message);

        var role = await _roleRepository.GetRoleByNameAsync(getRoleNameResult.AsT1);

        if (role is null) return new NotAppropriateRoleResult($"Role with name {getRoleNameResult} does not exist!");

        var passwordHash = PasswordHelpers.HashPassword(request.Password, out var salt);
        var user = new User(request.Username, request.Email, passwordHash, Convert.ToHexString(salt),
            request.PhoneNumber, role.Id);
        var token = TokenUtils.GenerateToken(64);
        user.AddUserToken(token, TokenType.AccountConfirmationToken);

        await _userRepository.AddUserAsync(user);
        await _publisher.Publish(new AccountConfirmationTokenAddedToUser(user.Id, token, request.Email),
            cancellationToken);

        return new NoContentResult();
    }

    private async Task<OneOf<NotAppropriateRoleResult, string>> GetRoleName(Guid? userId, string requestedRoleName)
    {
        const string userRoleName = "User";
        const string employeeRoleName = "Employee";
        const string administratorRoleName = "Administrator";

        var getUserResult = await _userRepository.GetUserByIdAsync(userId);

        if (getUserResult.IsT1)
            return requestedRoleName == userRoleName
                ? requestedRoleName
                : new NotAppropriateRoleResult("This role is not appropriate!");

        var isCurrentUserDefaultUser = getUserResult.AsT0.Role.Name == userRoleName;
        var isCurrentUserEmployee = getUserResult.AsT0.Role.Name == employeeRoleName;
        var isRequestedRoleAdministrator = requestedRoleName == administratorRoleName;

        if (isCurrentUserDefaultUser)
            return new NotAppropriateRoleResult("You cannot register new account being logged in!");
        if (isCurrentUserEmployee && !isRequestedRoleAdministrator) return requestedRoleName;
        if (isCurrentUserEmployee && isRequestedRoleAdministrator)
        {
            _logger.LogInformation(@"User {user} tried to create new account with role ""Administrator""",
                getUserResult);
            // TODO: Send notification to administrator about forbidden operation

            return new NotAppropriateRoleResult(
                "As employee, you cannot create new account with administrator role! Administrator will be notified about this attempt!");
        }

        return requestedRoleName;
    }
}