using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Results;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand,
    OneOf<NoContentResult, EmailInUseResult, NotExistingRoleResult, ForbiddenRoleResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task<OneOf<NoContentResult, EmailInUseResult, NotExistingRoleResult, ForbiddenRoleResult>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailUniqueAsync(request.Email))
            return new EmailInUseResult();

        var getRoleNameResult = await GetRoleName(request.UserId, request.RequestedRoleName);
        if (getRoleNameResult.TryPickT0(out var forbiddenRoleResult, out _))
            return new ForbiddenRoleResult(forbiddenRoleResult.Message);
        
        var role = await _roleRepository.GetRoleByNameAsync(getRoleNameResult.AsT1);

        if (role is null) return new NotExistingRoleResult($"Role with name {getRoleNameResult} does not exist!");

        var passwordHash = PasswordHelpers.HashPassword(request.Password, out var salt);
        var user = new User(request.Username, request.Email, passwordHash, Convert.ToHexString(salt),
            request.PhoneNumber, role.Id);

        await _userRepository.AddUserAsync(user);

        return new NoContentResult();
    }

    private async Task<OneOf<ForbiddenRoleResult, string>> GetRoleName(Guid? userId, string requestedRoleName)
    {
        const string userRoleName = "User";
        const string employeeRoleName = "Employee";
        const string administratorRoleName = "Administrator";

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return requestedRoleName == userRoleName
                ? requestedRoleName
                : new ForbiddenRoleResult("You cannot assign yourself this role!");

        var isCurrentUserDefaultUser = user.Role.Name == userRoleName;
        var isCurrentUserEmployee = user.Role.Name == employeeRoleName;
        var isRequestedRoleAdministrator = requestedRoleName == administratorRoleName;

        if (isCurrentUserDefaultUser)
            return new ForbiddenRoleResult("You cannot register new account being logged in!");
        if (isCurrentUserEmployee && !isRequestedRoleAdministrator) return requestedRoleName;
        if (isCurrentUserEmployee && isRequestedRoleAdministrator)
        {
            _logger.LogInformation(@"User {user} tried to create new account with role ""Administrator""", user);
            // TODO: Send notification to administrator about forbidden operation

            return new ForbiddenRoleResult(
                "As employee, you cannot create new account with administrator role! Administrator will be notified about this attempt!");
        }

        return requestedRoleName;
    }
}