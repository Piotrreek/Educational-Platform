using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class ExerciseSolutionConfiguration : IEntityTypeConfiguration<ExerciseSolution>
{
    public void Configure(EntityTypeBuilder<ExerciseSolution> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.FileName)
            .IsRequired();
        
        builder.Property(c => c.AverageRating)
            .HasPrecision(4, 3);
        
        builder.HasOne(c => c.Exercise)
            .WithMany(c => c.Solutions)
            .HasForeignKey(c => c.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(c => c.Reviews)
            .WithOne(c => c.ExerciseSolution)
            .HasForeignKey(c => c.ExerciseSolutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(ExerciseSolution.Reviews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}