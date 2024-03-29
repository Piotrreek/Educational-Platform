using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IAcademyRepository
{
    Task CreateUniversityAsync(string universityName);
    Task<OneOf<University, NotFound>> GetUniversityByNameAsync(string universityName);
    Task<OneOf<University, NotFound>> GetUniversityByIdAsync(Guid? universityId);
    Task<OneOf<UniversityCourse, NotFound>> GetUniversityCourseByIdAsync(Guid? universityCourseId);
    Task<OneOf<UniversitySubject, NotFound>> GetUniversitySubjectByIdAsync(Guid? universitySubjectId);
    Task<OneOf<Faculty, NotFound>> GetFacultyByIdAsync(Guid? facultyId);
    Task<IReadOnlyCollection<University>> GetAllUniversitiesAsync();
    Task CreateAcademyEntityRequestAsync(CreateAcademyEntityRequest request);
    Task<IEnumerable<CreateAcademyEntityRequest>> GetNotResolvedRequestsAsync();
    Task<OneOf<CreateAcademyEntityRequest, NotFound>> GetRequestByIdAsync(Guid id);
}