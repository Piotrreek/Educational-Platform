using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class UniversityConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.HasMany(u => u.Faculties)
            .WithOne(f => f.University)
            .HasForeignKey(f => f.UniversityId);

        builder.Metadata
            .FindNavigation(nameof(University.Faculties))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}