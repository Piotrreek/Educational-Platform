using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly EducationalPlatformDbContext _context;

    public RoleRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task AddRoleAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
    }

    public async Task<Role?> GetRoleByNameAsync(string? roleName)
    {
        if (roleName is null) return null;

        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == roleName);
    }
}