using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class DidacticMaterialRepository : IDidacticMaterialRepository
{
    private readonly DbSet<DidacticMaterial> _didacticMaterials;

    public DidacticMaterialRepository(DbSet<DidacticMaterial> didacticMaterials)
    {
        _didacticMaterials = didacticMaterials;
    }

    public async Task<OneOf<DidacticMaterial, NotFound>> GetDidacticMaterialByIdAsync(Guid? didacticMaterialId)
    {
        if (!didacticMaterialId.HasValue)
            return new NotFound();

        var didacticMaterial = await _didacticMaterials
            .Include(d => d.Ratings)
            .Include(d => d.Opinions)
            .ThenInclude(c => c.Author)
            .Include(e => e.Author)
            .Include(e => e.UniversityCourse)
            .ThenInclude(u => u.UniversitySubject)
            .ThenInclude(us => us.Faculty)
            .ThenInclude(f => f.University)
            .AsSingleQuery()
            .SingleOrDefaultAsync(d => d.Id == didacticMaterialId);

        return OneOfExtensions.GetValueOrNotFoundResult(didacticMaterial);
    }

    public async Task<IReadOnlyCollection<DidacticMaterial>> GetDidacticMaterials(Guid? universityId, Guid? facultyId,
        Guid? universitySubjectId, Guid? universityCourseId)
    {
        return await _didacticMaterials
            .Include(e => e.Author)
            .Include(e => e.Ratings)
            .AsSplitQuery()
            .Where(e => !universityId.HasValue ||
                        e.UniversityCourse.UniversitySubject.Faculty.University.Id == universityId)
            .Where(e => !facultyId.HasValue || e.UniversityCourse.UniversitySubject.Faculty.Id == facultyId)
            .Where(e => !universitySubjectId.HasValue || e.UniversityCourse.UniversitySubject.Id == universitySubjectId)
            .Where(e => !universityCourseId.HasValue || e.UniversityCourse.Id == universityCourseId)
            .ToListAsync();
    }
}