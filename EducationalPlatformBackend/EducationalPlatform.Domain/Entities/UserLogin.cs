using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class UserLogin : Entity
{
    public Guid UserId { get; }
    public bool IsSuccess { get; }

    public UserLogin(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private UserLogin()
    {
    }
}