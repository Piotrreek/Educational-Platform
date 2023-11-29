using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class CreateAcademyEntityRequestConfiguration : IEntityTypeConfiguration<CreateAcademyEntityRequest>
{
    public void Configure(EntityTypeBuilder<CreateAcademyEntityRequest> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever();

        builder.Property(d => d.EntityTypeName)
            .IsRequired();

        builder.Property(d => d.EntityName)
            .IsRequired();

        builder.Property(d => d.Status)
            .IsRequired()
            .HasDefaultValue(CreateAcademyEntityRequestStatus.Created)
            .HasConversion(d => d.ToString(),
                d => Enum.Parse<CreateAcademyEntityRequestStatus>(d));

        builder.HasOne(d => d.University)
            .WithMany()
            .HasForeignKey(d => d.UniversityId)
            .IsRequired(false);

        builder.HasOne(d => d.Faculty)
            .WithMany()
            .HasForeignKey(d => d.FacultyId)
            .IsRequired(false);

        builder.HasOne(d => d.UniversitySubject)
            .WithMany()
            .HasForeignKey(d => d.UniversitySubjectId)
            .IsRequired(false);

        builder.HasOne(d => d.Requester)
            .WithMany()
            .HasForeignKey(d => d.RequesterId)
            .IsRequired();
    }
}