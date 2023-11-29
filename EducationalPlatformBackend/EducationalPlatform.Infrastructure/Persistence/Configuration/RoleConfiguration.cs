using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .ValueGeneratedNever();

        builder
            .Property(r => r.Name)
            .IsRequired();

        builder
            .Property(r => r.CreatedOn)
            .IsRequired();

        builder
            .HasData(GetDefaultRoles());
    }

    private static Role[] GetDefaultRoles()
    {
        return new[]
        {
            new Role("Administrator", Guid.Parse("0151AD19-8241-4952-943B-DCC75D9A7600")),
            new Role("Employee", Guid.Parse("715D2298-BA94-4D0C-A94B-FD7B4054AD9F")),
            new Role("User", Guid.Parse("81A1E319-8958-457A-B59D-27BB0DCF0A06"))
        };
    }
}