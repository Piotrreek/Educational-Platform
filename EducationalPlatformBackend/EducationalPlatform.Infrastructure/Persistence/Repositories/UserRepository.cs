using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EducationalPlatformDbContext _context;

    public UserRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<OneOf<User, NotFound>> GetUserByEmailAsync(string? email)
    {
        if (email is null) return new NotFound();

        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user == null ? new NotFound() : user;
    }

    public async Task<OneOf<User, NotFound>> GetUserByIdAsync(Guid? userId)
    {
        if (userId is null) return new NotFound();

        var user = await _context.Users
            .Include(u => u.Role)
            .Include(u => u.UserTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user == null ? new NotFound() : user;
    }

    public async Task<bool> IsEmailUniqueAsync(string email) => (await GetUserByEmailAsync(email)).IsT1;
}