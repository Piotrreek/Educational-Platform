using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class DidacticMaterialConfiguration : IEntityTypeConfiguration<DidacticMaterial>
{
    public void Configure(EntityTypeBuilder<DidacticMaterial> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever();

        builder.Property(d => d.Name)
            .IsRequired();

        builder.Property(d => d.RatingsCount)
            .HasDefaultValue(0);

        builder.Property(d => d.AverageRating)
            .HasDefaultValue(0)
            .HasPrecision(1, 3);

        builder.Property(d => d.DidacticMaterialType)
            .IsRequired();

        builder.HasOne(d => d.UniversityCourse)
            .WithMany(c => c.DidacticMaterials)
            .HasForeignKey(d => d.UniversityCourseId);

        builder.HasOne(d => d.Author)
            .WithMany(u => u.DidacticMaterials)
            .HasForeignKey(d => d.AuthorId);
    }
}