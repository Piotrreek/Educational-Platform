using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class GeneralRepository : IGeneralRepository
{
    private readonly EducationalPlatformDbContext _context;

    public GeneralRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public void RollbackChanges()
    {
        foreach (var entry in _context.ChangeTracker.Entries<Entity>())
        {
            entry.State = EntityState.Unchanged;
        }
    }
}