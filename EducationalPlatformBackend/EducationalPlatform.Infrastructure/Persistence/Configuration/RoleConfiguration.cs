using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPlatform.Infrastructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
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
        return new[] { new Role("Administrator"), new Role("Employee"), new Role("User") };
    }
}