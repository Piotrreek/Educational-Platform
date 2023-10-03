using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;

    public RoleRepository(DbSet<Role> roles)
    {
        _roles = roles;
    }

    public async Task AddRoleAsync(Role role)
    {
        await _roles.AddAsync(role);
    }

    public async Task<Role?> GetRoleByNameAsync(string? roleName)
    {
        if (roleName is null) return null;

        return await _roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == roleName);
    }
}