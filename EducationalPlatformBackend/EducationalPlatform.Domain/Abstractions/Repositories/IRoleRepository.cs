using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IRoleRepository
{
    Task AddRoleAsync(Role role);
    Task<Role?> GetRoleByNameAsync(string? roleName);
}