using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class UserLogin : Entity
{
    public Guid UserId { get; }
    public bool IsSuccess { get; }

    public UserLogin(Guid userId, bool isSuccess)
    {
        UserId = userId;
        IsSuccess = isSuccess;
    }

    private UserLogin()
    {
    }
}