using EducationalPlatform.Application.Contracts.Exercise;
using MediatR;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutions;

public record GetExerciseSolutionsQuery(
    Guid ExerciseId,
    Guid? UserId
) : IRequest<IEnumerable<ExerciseSolutionDto>>;