using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public async Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByEmailAsync(string? email)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByIdAsync(Guid? userId)
    {
        throw new NotImplementedException();
    }
}