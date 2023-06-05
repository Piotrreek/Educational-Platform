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
    }
}