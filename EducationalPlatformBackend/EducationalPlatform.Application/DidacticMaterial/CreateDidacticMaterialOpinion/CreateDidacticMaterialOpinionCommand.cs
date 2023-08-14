using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialOpinion;

public record CreateDidacticMaterialOpinionCommand
    (Guid DidacticMaterialId, Guid UserId, string Opinion) : IRequest<
        OneOf<Success<IEnumerable<DidacticMaterialOpinionDto>>, BadRequestResult>>;