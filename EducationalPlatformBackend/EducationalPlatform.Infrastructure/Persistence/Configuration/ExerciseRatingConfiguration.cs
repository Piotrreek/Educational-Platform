using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class ExerciseRatingConfiguration : IEntityTypeConfiguration<ExerciseRating>
{
    public void Configure(EntityTypeBuilder<ExerciseRating> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Rating)
            .IsRequired();

        builder.HasOne(c => c.Exercise)
            .WithMany(c => c.Ratings)
            .HasForeignKey(c => c.ExerciseId);

        builder.HasOne(c => c.User)
            .WithMany(c => c.ExerciseRatings);
    }
}