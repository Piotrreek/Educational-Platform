using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionRating;

public record GetExerciseSolutionRatingQuery(
    Guid ExerciseSolutionId,
    Guid? UserId
) : IRequest<OneOf<SolutionRatingDto, BadRequestResult>>;