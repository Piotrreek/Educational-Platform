using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class DidacticMaterialRatingConfiguration : IEntityTypeConfiguration<DidacticMaterialRating>
{
    public void Configure(EntityTypeBuilder<DidacticMaterialRating> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.HasOne(r => r.DidacticMaterial)
            .WithMany(f => f.Ratings)
            .HasForeignKey(r => r.DidacticMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}