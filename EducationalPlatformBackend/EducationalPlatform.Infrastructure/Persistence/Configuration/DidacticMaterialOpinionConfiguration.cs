using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class DidacticMaterialOpinionConfiguration : IEntityTypeConfiguration<DidacticMaterialOpinion>
{
    public void Configure(EntityTypeBuilder<DidacticMaterialOpinion> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever();
        
        builder.Property(d => d.Opinion)
            .IsRequired();

        builder.HasOne(d => d.DidacticMaterial)
            .WithMany(d => d.Opinions)
            .HasForeignKey(d => d.DidacticMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Author)
            .WithMany(u => u.DidacticMaterialOpinions)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}