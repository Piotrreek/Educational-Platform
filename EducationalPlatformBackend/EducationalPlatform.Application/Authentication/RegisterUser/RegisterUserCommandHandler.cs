using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Primitives;
using MediatR;

namespace EducationalPlatform.Application.Authentication.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await EmailAlreadyTaken(request.Email))
            return Result.Fail("Account with given e-mail address has been already registered!");

        var roleName = await GetRoleName(request.UserId, request.RequestedRoleName);
        var role = await _roleRepository.GetRoleByNameAsync(roleName);

        if (role is null)
            return Result.Fail($"Role with name {roleName} does not exist or given role is forbidden in this context!");

        var passwordHash = PasswordHelpers.HashPassword(request.Password, out var salt);
        var user = new User(request.Username, request.Email, passwordHash, Convert.ToHexString(salt),
            request.PhoneNumber, role.Id);
        
        await _userRepository.AddUserAsync(user);

        return Result.Ok();
    }

    private async Task<bool> EmailAlreadyTaken(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email) is not null;
    }

    private async Task<string?> GetRoleName(Guid? userId, string requestedRoleName)
    {
        const string userRoleName = "User";
        const string employeeRoleName = "Employee";
        const string administratorRoleName = "Administrator";

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null && requestedRoleName == userRoleName)
            return userRoleName;

        if (user is null && requestedRoleName != userRoleName)
            return null;

        if (user!.Role.Name == employeeRoleName && requestedRoleName != administratorRoleName)
            return requestedRoleName;

        // TODO: Send notification to administrator about forbidden operation and use logger

        if (user.Role.Name == employeeRoleName && requestedRoleName == administratorRoleName)
            return null;

        return requestedRoleName;
    }
}