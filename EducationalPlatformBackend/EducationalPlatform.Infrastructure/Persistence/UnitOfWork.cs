using EducationalPlatform.Domain.Abstractions;
using EducationalPlatform.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly EducationalPlatformDbContext _dbContext;

    public UnitOfWork(EducationalPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateEntities(DateTimeOffset.Now);

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateEntities(DateTimeOffset dateTimeOffset)
    {
        foreach (var entityEntry in _dbContext.ChangeTracker.Entries<Entity>())
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Property(e => e.CreatedOn)
                        .CurrentValue = dateTimeOffset;
                    break;
                case EntityState.Modified:
                    entityEntry.Property(e => e.ModifiedOn)
                        .CurrentValue = dateTimeOffset;
                    break;
            }
        }
    }
}