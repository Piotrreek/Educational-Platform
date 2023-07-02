using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedNever();

        builder.HasOne(f => f.University)
            .WithMany(u => u.Faculties)
            .HasForeignKey(f => f.UniversityId);

        builder.HasMany(f => f.Subjects)
            .WithOne(s => s.Faculty)
            .HasForeignKey(s => s.FacultyId);
    }
}