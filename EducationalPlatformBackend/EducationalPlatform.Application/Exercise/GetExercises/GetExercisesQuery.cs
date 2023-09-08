using EducationalPlatform.Application.Contracts.Exercise;
using MediatR;

namespace EducationalPlatform.Application.Exercise.GetExercises;

public record GetExercisesQuery(string? Name) : IRequest<IEnumerable<BaseExerciseDto>>;