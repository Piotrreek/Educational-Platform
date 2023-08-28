using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolutionRating;

public record CreateExerciseSolutionRatingCommand
    (decimal Rating, Guid UserId, Guid SolutionId) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;