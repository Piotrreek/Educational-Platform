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
        // TODO: Check if there is user with e-mail specified in request
        // TODO: Use password hasher to hash password
        
        var role = await _roleRepository.GetRoleByNameAsync("User");
        
        var user = new User(request.Username, request.Email, request.Password, request.PhoneNumber, role.Id);

        await _userRepository.AddUserAsync(user);
        
        
        return Result.Ok();
    }
}