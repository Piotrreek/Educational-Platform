using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IDidacticMaterialRepository
{
    Task<OneOf<DidacticMaterial, NotFound>> GetDidacticMaterialByIdAsync(Guid? didacticMaterialId);

    Task<IReadOnlyCollection<DidacticMaterial>> GetDidacticMaterials(Guid? universityId, Guid? facultyId,
        Guid? universitySubjectId, Guid? universityCourseId);
}