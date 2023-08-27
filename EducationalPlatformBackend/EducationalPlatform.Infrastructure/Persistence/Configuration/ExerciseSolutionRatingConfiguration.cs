using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class ExerciseSolutionRatingConfiguration : IEntityTypeConfiguration<ExerciseSolutionRating>
{
    public void Configure(EntityTypeBuilder<ExerciseSolutionRating> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Rating)
            .IsRequired()
            .HasPrecision(4, 3);

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(c => c.ExerciseSolution)
            .WithMany(c => c.Ratings)
            .HasForeignKey(c => c.ExerciseSolutionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}