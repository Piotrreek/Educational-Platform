using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
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
            .Include(u => u.UserTokens)
            .FirstOrDefaultAsync(u => u.Email == email);

        return OneOfExtensions.GetValueOrNotFoundResult(user);
    }

    public async Task<OneOf<User, NotFound>> GetUserByIdAsync(Guid? userId)
    {
        if (userId is null) return new NotFound();

        var user = await _context.Users
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