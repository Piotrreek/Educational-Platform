using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IDidacticMaterialRepository
{
    Task<OneOf<DidacticMaterial, NotFound>> GetDidacticMaterialByIdAsync(Guid? didacticMaterialId);
}