using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
}