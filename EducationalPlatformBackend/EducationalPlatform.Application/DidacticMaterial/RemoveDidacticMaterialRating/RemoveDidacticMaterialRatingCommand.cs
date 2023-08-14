using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Application.DidacticMaterial.RemoveDidacticMaterialRating;

public record RemoveDidacticMaterialRatingCommand
    (Guid UserId, Guid DidacticMaterialId) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;