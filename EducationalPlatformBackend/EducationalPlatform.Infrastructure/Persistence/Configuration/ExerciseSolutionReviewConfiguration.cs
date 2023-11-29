using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class ExerciseSolutionReviewConfiguration : IEntityTypeConfiguration<ExerciseSolutionReview>
{
    public void Configure(EntityTypeBuilder<ExerciseSolutionReview> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.HasOne(c => c.ExerciseSolution)
            .WithMany(c => c.Reviews)
            .HasForeignKey(c => c.ExerciseSolutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}