using EducationalPlatform.Domain.Enums;
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

    public OneOf<Success, BadRequestResult> AddNewCourse(string courseName,
        UniversityCourseSession universityCourseSession)
    {
        if (string.IsNullOrWhiteSpace(courseName))
            return new BadRequestResult(UniversityCourseErrorMessages.EmptyName);

        if (_universityCourses.Any(f => f.Name == courseName))
            return new BadRequestResult(UniversityCourseErrorMessages.CourseWithNameAlreadyExists);

        var course = new UniversityCourse(courseName, universityCourseSession);
        _universityCourses.Add(course);

        return new Success();
    }

    // For EF
    private UniversitySubject()
    {
    }
}