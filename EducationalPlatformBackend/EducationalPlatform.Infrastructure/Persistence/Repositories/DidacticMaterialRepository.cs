using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class DidacticMaterialRepository : IDidacticMaterialRepository
{
    private readonly EducationalPlatformDbContext _context;

    public DidacticMaterialRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task CreateDidacticMaterial(DidacticMaterial didacticMaterial)
    {
        await _context.DidacticMaterials.AddAsync(didacticMaterial);
    }
}