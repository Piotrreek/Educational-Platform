using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.Commands.RemoveDidacticMaterialRating;

public record RemoveDidacticMaterialRatingCommand(
    Guid UserId,
    Guid DidacticMaterialId
) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;