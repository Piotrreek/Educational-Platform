using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever();

        builder
            .Property(u => u.UserName)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .HasIndex(u => u.Email);

        builder
            .Property(u => u.EmailConfirmed)
            .IsRequired();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Salt)
            .IsRequired();

        builder
            .Property(u => u.PhoneNumber)
            .IsRequired();

        builder
            .Property(u => u.CreatedOn)
            .IsRequired();

        builder
            .HasMany(u => u.UserLogins)
            .WithOne()
            .HasForeignKey(ul => ul.UserId);

        builder
            .HasMany(u => u.UserTokens)
            .WithOne()
            .HasForeignKey(ut => ut.UserId);

        builder
            .HasOne(u => u.Role)
            .WithMany();

        builder.HasOne(u => u.University)
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.UniversityId);

        builder.HasOne(u => u.Faculty)
            .WithMany(f => f.Users)
            .HasForeignKey(u => u.FacultyId);

        builder.HasOne(u => u.UniversitySubject)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.UniversitySubjectId);

        builder.HasMany(u => u.DidacticMaterials)
            .WithOne(d => d.Author)
            .HasForeignKey(d => d.AuthorId);

        builder.HasMany(u => u.DidacticMaterialOpinions)
            .WithOne(dm => dm.Author)
            .HasForeignKey(dm => dm.AuthorId);

        builder.HasMany(u => u.Ratings)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId);

        builder.Metadata
            .FindNavigation(nameof(User.DidacticMaterials))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.DidacticMaterialOpinions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.Ratings))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}