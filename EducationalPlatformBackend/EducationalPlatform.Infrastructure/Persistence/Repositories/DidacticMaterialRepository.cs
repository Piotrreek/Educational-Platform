using EducationalPlatform.Domain.Abstractions.Repositories;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class DidacticMaterialRepository : IDidacticMaterialRepository
{
    private readonly EducationalPlatformDbContext _context;

    public DidacticMaterialRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }
}