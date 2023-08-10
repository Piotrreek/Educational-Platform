using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Services;

public interface IJwtService
{
    string Generate(User user);
}