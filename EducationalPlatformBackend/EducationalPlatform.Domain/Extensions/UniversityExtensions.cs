using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Extensions;

public static class UniversityExtensions
{
    public static OneOf<Faculty, NotFound> GetFacultyById(this University university, Guid facultyId)
    {
        var faculty = university.Faculties.FirstOrDefault(f => f.Id == facultyId);

        return OneOfExtensions.GetValueOrNotFoundResult(faculty);
    }
}