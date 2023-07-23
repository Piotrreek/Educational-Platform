using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Authentication.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OneOf<UserDto, NotFound>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OneOf<UserDto, NotFound>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);

        return userResult.Match<OneOf<UserDto, NotFound>>(
            user => new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                UniversityName = user.University?.Name,
                FacultyName = user.Faculty?.Name,
                UniversitySubjectName = user.UniversitySubject?.Name
            },
            notFound => notFound
        );
    }
}