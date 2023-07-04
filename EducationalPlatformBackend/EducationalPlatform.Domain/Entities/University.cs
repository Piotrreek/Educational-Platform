using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class University : AcademyEntity
{
    public string Name { get; private set; } = null!;
    private readonly List<Faculty> _faculties = new();
    public IReadOnlyCollection<Faculty> Faculties => _faculties;

    public University(string name)
    {
        Name = name;
    }

    public OneOf<Success, BadRequestResult> AddNewFaculty(string facultyName)
    {
        if (string.IsNullOrWhiteSpace(facultyName))
            return new BadRequestResult(FacultyErrorMessages.EmptyName);

        if (_faculties.Any(f => f.Name == facultyName))
            return new BadRequestResult(FacultyErrorMessages.FacultyWithNameAlreadyExists);

        var faculty = new Faculty(facultyName);
        _faculties.Add(faculty);

        return new Success();
    }

    protected override bool UserAlreadyAssignedToOtherAcademyEntity(User user) => user.UniversityId.HasValue && user.UniversityId != Id;
    protected override bool UserAlreadyAssignedToIdenticalAcademyEntity(User user) => user.UniversityId.HasValue && user.UniversityId == Id;
    protected override string UserAlreadyAssignedToOtherAcademyEntityMessage() => UniversityErrorMessages.UserAlreadyAssignedToUniversity;
    protected override string UserAlreadyAssignedToIdenticalAcademyEntityMessage() => UniversityErrorMessages.UserAlreadyInSameUniversity;
    protected override string UserNotInIdenticalAcademyEntityMessage() => UniversityErrorMessages.UserNotInUniversity;

    // For EF
    private University()
    {
    }
}