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
    public DbSet<DidacticMaterial> DidacticMaterials => Set<DidacticMaterial>();
    public DbSet<DidacticMaterialOpinion> DidacticMaterialOpinions => Set<DidacticMaterialOpinion>();
    public DbSet<DidacticMaterialRating> DidacticMaterialRatings => Set<DidacticMaterialRating>();
    public DbSet<CreateAcademyEntityRequest> CreateAcademyEntityRequests => Set<CreateAcademyEntityRequest>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseRating> ExerciseRatings => Set<ExerciseRating>();
    public DbSet<ExerciseComment> ExerciseComments => Set<ExerciseComment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}