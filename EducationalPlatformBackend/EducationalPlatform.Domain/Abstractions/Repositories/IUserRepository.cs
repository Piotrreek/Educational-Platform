using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string? email);
    Task<User?> GetUserByIdAsync(Guid? userId);
    Task<bool> IsEmailUniqueAsync(string email);
}