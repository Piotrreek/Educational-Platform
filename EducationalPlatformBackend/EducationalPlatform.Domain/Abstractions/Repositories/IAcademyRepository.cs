using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IAcademyRepository
{
    Task CreateUniversityAsync(string universityName);
    Task<OneOf<University, NotFound>> GetUniversityByNameAsync(string universityName);
    Task<OneOf<University, NotFound>> GetUniversityByIdAsync(Guid? universityId);
}