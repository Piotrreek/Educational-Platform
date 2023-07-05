using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IAcademyRepository
{
    Task CreateUniversityAsync(string universityName);
    Task<University?> GetUniversityByNameAsync(string universityName);
}