using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence;

public class EducationalPlatformDbContext : DbContext
{
    public EducationalPlatformDbContext(DbContextOptions options) : base(options)
    {
    }

    private EducationalPlatformDbContext()
    {
    }


    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserLogin> UserLogins => Set<UserLogin>();
    public DbSet<UserToken> UserTokens => Set<UserToken>();
    public DbSet<University> Universities => Set<University>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<UniversitySubject> UniversitySubjects => Set<UniversitySubject>();
    public DbSet<UniversityCourse> UniversityCourses => Set<UniversityCourse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}