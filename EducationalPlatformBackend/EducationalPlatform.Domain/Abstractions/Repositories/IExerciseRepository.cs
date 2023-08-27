using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IExerciseRepository
{
    Task AddExerciseAsync(Exercise exercise);
    Task<OneOf<Exercise, NotFound>> GetExerciseByIdAsync(Guid id);
    Task<OneOf<ExerciseSolution, NotFound>> GetExerciseSolutionByIdAsync(Guid id);
}