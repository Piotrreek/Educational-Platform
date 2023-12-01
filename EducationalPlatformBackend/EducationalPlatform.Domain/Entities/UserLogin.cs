using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public sealed class UserLogin : Entity
{
    public Guid UserId { get; }
    public bool IsSuccess { get; }

    private UserLogin(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static UserLogin Create(bool isSuccess) => new(isSuccess);

    private UserLogin()
    {
    }
}