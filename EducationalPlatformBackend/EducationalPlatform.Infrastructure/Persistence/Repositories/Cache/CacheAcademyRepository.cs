using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using OneOf.Types;
using CacheExtensions = EducationalPlatform.Domain.Extensions.CacheExtensions;

namespace EducationPlatform.Infrastructure.Persistence.Repositories.Cache;

public class CacheAcademyRepository : IAcademyRepository
{
    private readonly IAcademyRepository _academyRepository;
    private readonly IMemoryCache _cache;

    public CacheAcademyRepository(IAcademyRepository academyRepository, IMemoryCache cache)
    {
        _academyRepository = academyRepository;
        _cache = cache;
    }

    public async Task CreateUniversityAsync(string universityName)
    {
        await _academyRepository.CreateUniversityAsync(universityName);
        _cache.Remove($"university-${universityName}");
        _cache.Remove("universities");
    }

    public async Task<OneOf<University, NotFound>> GetUniversityByNameAsync(string universityName)
    {
        var cacheKey = $"university-${universityName}";

        if (!_cache.TryGetValue(cacheKey, out OneOf<University, NotFound> result))
        {
            result = await _academyRepository.GetUniversityByNameAsync(universityName);
            _cache.Set(cacheKey, result, CacheExtensions.DefaultCacheEntryOptions);
        }

        return result;
    }

    public async Task<OneOf<University, NotFound>> GetUniversityByIdAsync(Guid? universityId)
    {
        return await _academyRepository.GetUniversityByIdAsync(universityId);
    }

    public async Task<OneOf<UniversityCourse, NotFound>> GetUniversityCourseByIdAsync(Guid? universityCourseId)
    {
        return await _academyRepository.GetUniversityCourseByIdAsync(universityCourseId);
    }

    public async Task<OneOf<UniversitySubject, NotFound>> GetUniversitySubjectByIdAsync(Guid? universitySubjectId)
    {
        return await _academyRepository.GetUniversitySubjectByIdAsync(universitySubjectId);
    }

    public async Task<OneOf<Faculty, NotFound>> GetFacultyByIdAsync(Guid? facultyId)
    {
        return await _academyRepository.GetFacultyByIdAsync(facultyId);
    }

    public async Task<IReadOnlyCollection<University>> GetAllUniversitiesAsync()
    {
        var cacheKey = "universities";
        if (!_cache.TryGetValue(cacheKey, out IReadOnlyCollection<University>? universities))
        {
            universities = await _academyRepository.GetAllUniversitiesAsync();
            _cache.Set(cacheKey, universities, CacheExtensions.DefaultCacheEntryOptions);
        }

        return universities!;
    }

    public async Task CreateAcademyEntityRequestAsync(CreateAcademyEntityRequest request)
    {
        await _academyRepository.CreateAcademyEntityRequestAsync(request);
    }

    public async Task<IEnumerable<CreateAcademyEntityRequest>> GetNotResolvedRequestsAsync()
    {
        return await _academyRepository.GetNotResolvedRequestsAsync();
    }

    public async Task<OneOf<CreateAcademyEntityRequest, NotFound>> GetRequestByIdAsync(Guid id)
    {
        return await _academyRepository.GetRequestByIdAsync(id);
    }
}