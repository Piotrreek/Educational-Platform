using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class DidacticMaterialOpinionConfiguration : IEntityTypeConfiguration<DidacticMaterialOpinion>
{
    public void Configure(EntityTypeBuilder<DidacticMaterialOpinion> builder)
    {
        builder.Property(d => d.Opinion)
            .IsRequired();

        builder.HasOne(d => d.DidacticMaterial)
            .WithMany(d => d.Opinions)
            .HasForeignKey(d => d.DidacticMaterialId);

        builder.HasOne(d => d.Author)
            .WithMany(u => u.DidacticMaterialOpinions)
            .HasForeignKey(d => d.AuthorId);
    }
}