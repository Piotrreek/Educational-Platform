using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Entities;

public class University : AcademyEntity
{
    private readonly List<Faculty> _faculties = new();
    public IReadOnlyCollection<Faculty> Faculties => _faculties;

    private University(string name) : base(name)
    {
    }

    public static University Create(string name) => new(name);
    
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

    // For EF
    private University()
    {
    }
}