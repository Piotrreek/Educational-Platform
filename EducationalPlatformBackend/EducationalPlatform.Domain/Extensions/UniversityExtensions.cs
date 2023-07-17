using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Extensions;

public static class UniversityExtensions
{
    public static OneOf<Faculty, NotFound> GetFacultyById(this University university, Guid? facultyId)
    {
        var faculty = university.Faculties.FirstOrDefault(f => f.Id == facultyId);

        return OneOfExtensions.GetValueOrNotFoundResult(faculty);
    }

    public static OneOf<UniversitySubject, NotFound> GetUniversitySubjectById(this University university,
        Guid? facultyId, Guid? subjectId)
    {
        var facultyResult = university.GetFacultyById(facultyId);
        if (facultyResult.IsT1)
            return facultyResult.AsT1;

        var subject = facultyResult.AsT0.UniversitySubjects.FirstOrDefault(s => s.Id == subjectId);

        return OneOfExtensions.GetValueOrNotFoundResult(subject);
    }
}