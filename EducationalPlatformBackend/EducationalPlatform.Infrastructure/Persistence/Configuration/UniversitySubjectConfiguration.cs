using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class UniversitySubjectConfiguration : IEntityTypeConfiguration<UniversitySubject>
{
    public void Configure(EntityTypeBuilder<UniversitySubject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Name)
            .IsRequired();

        builder.Property(s => s.UniversitySubjectDegree)
            .IsRequired();

        builder.HasOne(s => s.Faculty)
            .WithMany(f => f.UniversitySubjects)
            .HasForeignKey(s => s.FacultyId);

        builder.HasMany(s => s.Users)
            .WithOne(u => u.Subject)
            .HasForeignKey(u => u.SubjectId);

        builder.HasMany(s => s.UniversityCourses)
            .WithOne(c => c.UniversitySubject)
            .HasForeignKey(c => c.UniversitySubjectId);

        builder.Metadata
            .FindNavigation(nameof(UniversitySubject.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(UniversitySubject.UniversityCourses))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}