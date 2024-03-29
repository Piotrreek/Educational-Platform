using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRating;

public record CreateDidacticMaterialRatingCommand
    (decimal Rating, Guid UserId, Guid DidacticMaterialId) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;