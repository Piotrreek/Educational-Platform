using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Entities;

public class Faculty : AcademyEntity
{
    public string Name { get; private set; } = null!;
    private readonly List<UniversitySubject> _universitySubjects = new();
    public IReadOnlyCollection<UniversitySubject> UniversitySubjects => _universitySubjects;
    public University University { get; private set; } = null!;
    public Guid UniversityId { get; private set; }

    internal Faculty(string name)
    {
        Name = name;
    }

    public OneOf<Success, BadRequestResult> AddNewSubject(string subjectName, UniversitySubjectDegree degree)
    {
        if (string.IsNullOrWhiteSpace(subjectName))
            return new BadRequestResult(UniversitySubjectErrorMessages.EmptyName);

        if (_universitySubjects.Any(f => f.Name == subjectName))
            return new BadRequestResult(UniversitySubjectErrorMessages.SubjectWithNameAlreadyExists);

        var subject = new UniversitySubject(subjectName, degree);
        _universitySubjects.Add(subject);

        return new Success();
    }

    protected override bool UserAlreadyAssignedToOtherAcademyEntity(User user) => 
        user.UniversitySubjectId.HasValue ||
        (user.FacultyId.HasValue && user.FacultyId != Id);

    protected override bool UserAlreadyAssignedToIdenticalAcademyEntity(User user) =>
        user.FacultyId.HasValue && user.FacultyId == Id;

    protected override string UserAlreadyAssignedToOtherAcademyEntityMessage() =>
        FacultyErrorMessages.UserAlreadyAssignedToFaculty;

    protected override string UserAlreadyAssignedToIdenticalAcademyEntityMessage() =>
        FacultyErrorMessages.UserAlreadyInSameFaculty;

    protected override string UserNotInIdenticalAcademyEntityMessage() => FacultyErrorMessages.UserNotInFaculty;

    // For EF
    private Faculty()
    {
    }
}