using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.Abstractions.Services;

public interface IJwtService
{
    string Generate(User user);
}