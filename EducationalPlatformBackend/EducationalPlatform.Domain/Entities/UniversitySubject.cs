using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Entities;

public class UniversitySubject : AcademyEntity
{
    public string Name { get; private set; } = null!;
    public UniversitySubjectDegree UniversitySubjectDegree { get; private set; }
    public Faculty Faculty { get; private set; } = null!;
    public Guid FacultyId { get; private set; }
    private readonly List<UniversityCourse> _universityCourses = new();
    public IReadOnlyCollection<UniversityCourse> UniversityCourses => _universityCourses;
    
    internal UniversitySubject(string name, UniversitySubjectDegree universitySubjectDegree)
    {
        Name = name;
        UniversitySubjectDegree = universitySubjectDegree;
    }
    
    public OneOf<Success, BadRequestResult> AddNewCourse(string name, UniversityCourseSession universityCourseSession)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestResult(UniversityCourseErrorMessages.EmptyName);

        var course = new UniversityCourse(name, universityCourseSession);
        _universityCourses.Add(course);

        return new Success();
    }

    protected override bool UserAlreadyAssignedToAcademyEntity(User user) => user.UniversitySubjectId.HasValue;
    protected override string UserAlreadyAssignedToAcademyEntityMessage() => UniversitySubjectErrorMessages.UserAlreadyAssignedToSubject;
    protected override string UserAlreadyAssignedToIdenticalAcademyEntityMessage() => UniversitySubjectErrorMessages.UserAlreadyInSameSubject;
    protected override string UserNotInIdenticalAcademyEntityMessage() => UniversitySubjectErrorMessages.UserNotInUniversitySubject;

    // For EF
    private UniversitySubject()
    {
    }
}