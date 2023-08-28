using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.RemoveExerciseSolutionRating;

public record RemoveExerciseSolutionRatingCommand
    (Guid SolutionId, Guid UserId) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;