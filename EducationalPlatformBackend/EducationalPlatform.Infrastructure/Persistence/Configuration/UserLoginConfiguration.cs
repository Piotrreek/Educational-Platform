using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder
            .HasKey(ul => ul.Id);

        builder
            .Property(ul => ul.Id)
            .ValueGeneratedNever();

        builder
            .Property(ul => ul.IsSuccess)
            .IsRequired();

        builder
            .Property(ul => ul.CreatedOn)
            .IsRequired();
    }
}