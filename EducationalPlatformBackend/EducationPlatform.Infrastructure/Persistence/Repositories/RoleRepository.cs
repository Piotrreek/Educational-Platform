using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    public async Task AddRoleAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public async Task<Role> GetRoleByNameAsync(string roleName)
    {
        throw new NotImplementedException();
    }
}