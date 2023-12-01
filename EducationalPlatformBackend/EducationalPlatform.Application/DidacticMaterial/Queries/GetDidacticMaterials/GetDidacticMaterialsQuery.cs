using EducationalPlatform.Application.Contracts.DidacticMaterial;
using MediatR;

namespace EducationalPlatform.Application.DidacticMaterial.Queries.GetDidacticMaterials;

public record GetDidacticMaterialsQuery(
    Guid? UniversityId,
    Guid? FacultyId,
    Guid? UniversitySubjectId,
    Guid? UniversityCourseId
) : IRequest<IEnumerable<DidacticMaterialDto>>;