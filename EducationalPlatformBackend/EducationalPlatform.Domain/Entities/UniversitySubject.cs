using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Entities;

public class UniversitySubject : Entity
{
    public string Name { get; private set; } = null!;
    public UniversitySubjectDegree UniversitySubjectDegree { get; private set; }
    public Faculty Faculty { get; private set; } = null!;
    public Guid FacultyId { get; private set; }
    private readonly List<UniversityCourse> _universityCourses = new();
    public IReadOnlyCollection<UniversityCourse> UniversityCourses => _universityCourses;
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    internal UniversitySubject(string name, UniversitySubjectDegree universitySubjectDegree)
    {
        Name = name;
        UniversitySubjectDegree = universitySubjectDegree;
    }

    public OneOf<Success, BadRequestResult> AssignUser(User user)
    {
        if (user.UniversitySubjectId.HasValue)
            return new BadRequestResult(ErrorMessages.UserAlreadyAssignedToSubject);

        if (_users.Any(u => u.Id == user.Id))
            return new BadRequestResult(ErrorMessages.UserAlreadyInSameSubject);

        _users.Add(user);

        return new Success();
    }

    public OneOf<Success, BadRequestResult> AddNewCourse(string name, UniversityCourseSession universityCourseSession)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestResult(ErrorMessages.UniversityCourseNameEmptyErrorMessage);

        var course = new UniversityCourse(name, universityCourseSession);
        _universityCourses.Add(course);

        return new Success();
    }

    // For EF
    private UniversitySubject()
    {
    }
}