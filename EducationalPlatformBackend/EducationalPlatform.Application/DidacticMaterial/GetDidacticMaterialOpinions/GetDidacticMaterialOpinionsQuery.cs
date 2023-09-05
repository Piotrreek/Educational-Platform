using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialOpinions;

public record GetDidacticMaterialOpinionsQuery
    (Guid DidacticMaterialId) : IRequest<OneOf<IEnumerable<OpinionDto>, BadRequestResult>>;