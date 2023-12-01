using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Commands.RemoveExerciseRating;

public record RemoveExerciseRatingCommand(
    Guid ExerciseId,
    Guid UserId
) : IRequest<OneOf<Success<RatingDto>, BadRequestResult>>;