using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Results;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Domain.Primitives;

public abstract class AcademyEntity : Entity
{
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;
    
    public OneOf<Success, BadRequestResult> RemoveUser(User user)
    {
        var removeResult = _users.Remove(user);
        
        if (!removeResult)
            return new BadRequestResult(UserNotInIdenticalAcademyEntityMessage());
        
        return new Success();
    }
    
    public OneOf<Success, BadRequestResult> AssignUser(User user)
    {
        if (UserAlreadyAssignedToOtherAcademyEntity(user))
            return new BadRequestResult(UserAlreadyAssignedToOtherAcademyEntityMessage());

        if (UserAlreadyAssignedToIdenticalAcademyEntity(user))
            return new BadRequestResult(UserAlreadyAssignedToIdenticalAcademyEntityMessage());

        _users.Add(user);

        return new Success();
    }

    protected abstract bool UserAlreadyAssignedToOtherAcademyEntity(User user);
    protected abstract bool UserAlreadyAssignedToIdenticalAcademyEntity(User user);
    protected abstract string UserAlreadyAssignedToOtherAcademyEntityMessage();
    protected abstract string UserAlreadyAssignedToIdenticalAcademyEntityMessage();
    protected abstract string UserNotInIdenticalAcademyEntityMessage();

}