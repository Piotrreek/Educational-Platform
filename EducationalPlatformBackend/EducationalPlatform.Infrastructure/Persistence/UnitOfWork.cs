using EducationalPlatform.Domain.Abstractions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
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

    public void Rollback()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries<Entity>())
        {
            entry.State = EntityState.Detached;
        }
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