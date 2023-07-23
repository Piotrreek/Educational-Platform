using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IDidacticMaterialRepository
{
    Task CreateDidacticMaterial(DidacticMaterial didacticMaterial);
}