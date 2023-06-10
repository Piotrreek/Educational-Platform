using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
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

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailUniqueAsync(request.Email))
            return Result.Fail("Account with given e-mail address has been already registered!",
                StatusCodes.Status400BadRequest);

        var getRoleNameResult = await GetRoleName(request.UserId, request.RequestedRoleName);
        if (getRoleNameResult.IsFailure) return getRoleNameResult;

        var roleName = getRoleNameResult.Value();

        var role = await _roleRepository.GetRoleByNameAsync(roleName);

        if (role is null)
            return Result.Fail($"Role with name {roleName} does not exist or given role is forbidden in this context!",
                StatusCodes.Status400BadRequest);

        var passwordHash = PasswordHelpers.HashPassword(request.Password, out var salt);
        var user = new User(request.Username, request.Email, passwordHash, Convert.ToHexString(salt),
            request.PhoneNumber, role.Id);

        await _userRepository.AddUserAsync(user);

        return Result.Ok(StatusCodes.Status204NoContent);
    }
    
    private async Task<Result<string>> GetRoleName(Guid? userId, string requestedRoleName)
    {
        const string userRoleName = "User";
        const string employeeRoleName = "Employee";
        const string administratorRoleName = "Administrator";

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return requestedRoleName == userRoleName
                ? GetOkResult()
                : GetForbiddenResult();

        var isCurrentUserDefaultUser = user.Role.Name == userRoleName;
        var isCurrentUserEmployee = user.Role.Name == employeeRoleName;
        var isRequestedRoleAdministrator = requestedRoleName == administratorRoleName;

        if (isCurrentUserDefaultUser) GetForbiddenResult();
        if (isCurrentUserEmployee && !isRequestedRoleAdministrator) return GetOkResult();
        if (isCurrentUserEmployee && isRequestedRoleAdministrator)
        {
            _logger.LogInformation(@"User {user} tried to create new account with role ""Administrator""", user);
            // TODO: Send notification to administrator about forbidden operation

            return GetForbiddenResult();
        }

        return GetOkResult();

        Result<string> GetOkResult() => Result.Ok(requestedRoleName, StatusCodes.Status204NoContent);

        Result<string> GetForbiddenResult() => Result.Fail<string>("You are not authorized to perform this operation!",
            StatusCodes.Status403Forbidden);
    }
}