using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories.Cache;

internal sealed class CacheDidacticMaterialRepository : IDidacticMaterialRepository
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;
    private readonly IMemoryCache _cache;

    public CacheDidacticMaterialRepository(IDidacticMaterialRepository didacticMaterialRepository, IMemoryCache cache)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
        _cache = cache;
    }

    public async Task<OneOf<DidacticMaterial, NotFound>> GetDidacticMaterialByIdAsync(Guid? didacticMaterialId)
    {
        return await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(didacticMaterialId);
    }

    public async Task<IReadOnlyCollection<DidacticMaterial>> GetDidacticMaterials(Guid? universityId, Guid? facultyId,
        Guid? universitySubjectId, Guid? universityCourseId)
    {
        var cacheKey =
            $"didactic-materials-${universityId?.ToString()}-${facultyId?.ToString()}-${universitySubjectId?.ToString()}-${universityCourseId?.ToString()}";
        return await _cache.GetOrSaveAndGet(cacheKey,
            () => _didacticMaterialRepository.GetDidacticMaterials(universityId, facultyId, universitySubjectId,
                universityCourseId));
    }
}