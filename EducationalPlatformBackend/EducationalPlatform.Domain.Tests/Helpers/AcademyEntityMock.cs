using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Tests.Helpers;

public class AcademyEntityMock : AcademyEntity
{
    public bool UserAlreadyAssignedToAcademyEntityProperty { get; set; }

    protected override bool UserAlreadyAssignedToAcademyEntity(User user) => UserAlreadyAssignedToAcademyEntityProperty;
    protected override string UserAlreadyAssignedToAcademyEntityMessage() => "123";
    protected override string UserAlreadyAssignedToIdenticalAcademyEntityMessage() => "1234";
    protected override string UserNotInIdenticalAcademyEntityMessage() => "12345";
}