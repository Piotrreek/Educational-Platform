using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExerciseSolutionRating;

public record CreateExerciseSolutionRatingCommand(
    decimal Rating,
    Guid UserId,
    Guid SolutionId
) : IRequest<OneOf<Success, BadRequestResult>>;