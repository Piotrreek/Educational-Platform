using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class ExerciseCommentConfiguration : IEntityTypeConfiguration<ExerciseComment>
{
    public void Configure(EntityTypeBuilder<ExerciseComment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Comment)
            .IsRequired();

        builder.HasOne(c => c.Exercise)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}