using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class DidacticMaterialRepository : IDidacticMaterialRepository
{
    private readonly EducationalPlatformDbContext _context;

    public DidacticMaterialRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<DidacticMaterial, NotFound>> GetDidacticMaterialByIdAsync(Guid? didacticMaterialId)
    {
        if (!didacticMaterialId.HasValue)
            return new NotFound();

        var didacticMaterial = await _context.DidacticMaterials
            .Include(d => d.Ratings)
            .Include(d => d.Opinions)
            .SingleOrDefaultAsync(d => d.Id == didacticMaterialId);

        return OneOfExtensions.GetValueOrNotFoundResult(didacticMaterial);
    }
}