using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedNever();

        builder.Property(f => f.Name)
            .IsRequired();
        
        builder.HasOne(f => f.University)
            .WithMany(u => u.Faculties)
            .HasForeignKey(f => f.UniversityId);

        builder.HasMany(f => f.UniversitySubjects)
            .WithOne(s => s.Faculty)
            .HasForeignKey(s => s.FacultyId);

        builder.HasMany(f => f.Users)
            .WithOne(u => u.Faculty)
            .HasForeignKey(u => u.FacultyId);

        builder.Metadata
            .FindNavigation(nameof(Faculty.UniversitySubjects))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Faculty.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}