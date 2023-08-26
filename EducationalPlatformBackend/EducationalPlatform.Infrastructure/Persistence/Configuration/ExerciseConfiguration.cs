using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.FileName)
            .IsRequired();

        builder.HasOne(c => c.Author)
            .WithMany(c => c.Exercises)
            .HasForeignKey(c => c.AuthorId);

        builder.HasMany(c => c.Comments)
            .WithOne(c => c.Exercise)
            .HasForeignKey(c => c.ExerciseId);

        builder.HasMany(c => c.Ratings)
            .WithOne(c => c.Exercise)
            .HasForeignKey(c => c.ExerciseId);

        builder.Metadata
            .FindNavigation(nameof(Exercise.Ratings))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Exercise.Comments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}