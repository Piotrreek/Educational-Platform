using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class University : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<Faculty> _faculties = new();
    public IReadOnlyCollection<Faculty> Faculties => _faculties;
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public University(string name)
    {
        Name = name;
    }

    public OneOf<Success, BadRequestResult> AssignUser(User user)
    {
        if (user.UniversityId.HasValue)
            return new BadRequestResult(ErrorMessages.UserAlreadyAssignedToUniversity);

        if (_users.Any(u => u.Id == user.Id))
            return new BadRequestResult(ErrorMessages.UserAlreadyInSameUniversity);

        _users.Add(user);

        return new Success();
    }

    public OneOf<Success, BadRequestResult> AddNewFaculty(string facultyName)
    {
        if (string.IsNullOrWhiteSpace(facultyName))
            return new BadRequestResult(ErrorMessages.FacultyNameEmptyErrorMessage);

        var faculty = new Faculty(facultyName);
        _faculties.Add(faculty);

        return new Success();
    }

    // For EF
    private University()
    {
    }
}