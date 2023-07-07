using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class UniversityCourseConfiguration : IEntityTypeConfiguration<UniversityCourse>
{
    public void Configure(EntityTypeBuilder<UniversityCourse> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.UniversityCourseSession)
            .IsRequired();

        builder.HasOne(c => c.UniversitySubject)
            .WithMany(s => s.UniversityCourses)
            .HasForeignKey(c => c.UniversitySubjectId);
    }
}