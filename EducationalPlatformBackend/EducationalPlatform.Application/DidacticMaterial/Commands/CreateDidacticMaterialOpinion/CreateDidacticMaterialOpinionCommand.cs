using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.Commands.CreateDidacticMaterialOpinion;

public record CreateDidacticMaterialOpinionCommand(
    Guid DidacticMaterialId,
    Guid UserId,
    string Opinion
) : IRequest<OneOf<Success<IEnumerable<OpinionDto>>, BadRequestResult>>;