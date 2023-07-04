using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Entities;

public class Faculty : Entity
{
    public string Name { get; private set; } = null!;
    private readonly List<UniversitySubject> _universitySubjects = new();
    public IReadOnlyCollection<UniversitySubject> UniversitySubjects => _universitySubjects;
    public University University { get; private set; } = null!;
    public Guid UniversityId { get; private set; }
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    internal Faculty(string name)
    {
        Name = name;
    }

    public OneOf<Success, BadRequestResult> AssignUser(User user)
    {
        if (user.FacultyId.HasValue)
            return new BadRequestResult(ErrorMessages.UserAlreadyAssignedToFaculty);

        if (_users.Any(u => u.Id == user.Id))
            return new BadRequestResult(ErrorMessages.UserAlreadyInSameFaculty);

        _users.Add(user);

        return new Success();
    }

    public OneOf<Success, BadRequestResult> AddNewSubject(string name, UniversitySubjectDegree degree)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestResult(ErrorMessages.SubjectNameEmptyErrorMessage);
        
        var subject = new UniversitySubject(name, degree);
        _universitySubjects.Add(subject);

        return new Success();
    }

    // For EF
    private Faculty()
    {
    }
}