using EducationalPlatform.Application.Contracts.DidacticMaterial;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterial;

public record GetDidacticMaterialQuery(
    Guid DidacticMaterialId,
    Guid? UserId
) : IRequest<OneOf<DetailedDidacticMaterialDto, NotFound>>;