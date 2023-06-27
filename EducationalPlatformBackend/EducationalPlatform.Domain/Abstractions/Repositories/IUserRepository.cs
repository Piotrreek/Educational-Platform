using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<OneOf<User, NotFound>> GetUserByEmailAsync(string? email);
    Task<OneOf<User, NotFound>> GetUserByIdAsync(Guid? userId);
    Task<bool> IsEmailUniqueAsync(string email);
}