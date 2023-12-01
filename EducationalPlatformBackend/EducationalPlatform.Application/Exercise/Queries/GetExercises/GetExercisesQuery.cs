using EducationalPlatform.Application.Contracts.Exercise;
using MediatR;

namespace EducationalPlatform.Application.Exercise.Queries.GetExercises;

public record GetExercisesQuery(string? Name) : IRequest<IEnumerable<BaseExerciseDto>>;