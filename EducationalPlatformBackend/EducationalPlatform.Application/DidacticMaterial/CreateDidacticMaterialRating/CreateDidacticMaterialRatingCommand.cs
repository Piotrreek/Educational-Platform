using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRate;

public record CreateDidacticMaterialRatingCommand
    (int Rating, Guid UserId, Guid DidacticMaterialId) : IRequest<OneOf<Success<AverageRatingDto>, BadRequestResult>>;