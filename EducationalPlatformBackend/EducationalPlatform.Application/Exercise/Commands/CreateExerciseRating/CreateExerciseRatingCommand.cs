using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExerciseRating;

public record CreateExerciseRatingCommand(
    Guid ExerciseId,
    Guid UserId,
    decimal Rating
) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;