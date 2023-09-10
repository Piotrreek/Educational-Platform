using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.GetExerciseSolutionReviews;

public record GetExerciseSolutionReviewsQuery
    (Guid ExerciseSolutionId) : IRequest<OneOf<IEnumerable<ExerciseSolutionReviewDto>, BadRequestResult>>;