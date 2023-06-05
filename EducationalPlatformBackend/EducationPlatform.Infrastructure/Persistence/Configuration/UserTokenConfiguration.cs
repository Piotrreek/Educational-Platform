using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder
            .HasKey(ut => ut.Id);

        builder
            .Property(ut => ut.Id)
            .ValueGeneratedNever();

        builder
            .Property(ut => ut.TokenType)
            .IsRequired();

        builder
            .Property(ut => ut.ExpirationDateTimeOffset)
            .IsRequired();

        builder
            .Property(ut => ut.Token)
            .IsRequired();

        builder
            .Property(ut => ut.CreatedOn)
            .IsRequired();
    }
}