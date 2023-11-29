using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(DbSet<User> users)
    {
        _users = users;
    }

    public async Task AddUserAsync(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<OneOf<User, NotFound>> GetUserByEmailAsync(string? email)
    {
        if (email is null) return new NotFound();

        var user = await _users
            .Include(u => u.Role)
            .Include(u => u.UserTokens)
            .FirstOrDefaultAsync(u => u.Email == email);

        return OneOfExtensions.GetValueOrNotFoundResult(user);
    }

    public async Task<OneOf<User, NotFound>> GetUserByIdAsync(Guid? userId)
    {
        if (userId is null) return new NotFound();

        var user = await _users
            .Include(u => u.Role)
            .Include(u => u.UserTokens)
            .Include(u => u.University)
            .Include(u => u.Faculty)
            .Include(u => u.UniversitySubject)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return OneOfExtensions.GetValueOrNotFoundResult(user);
    }

    public async Task<bool> IsEmailUniqueAsync(string email) => (await GetUserByEmailAsync(email)).IsT1;
}