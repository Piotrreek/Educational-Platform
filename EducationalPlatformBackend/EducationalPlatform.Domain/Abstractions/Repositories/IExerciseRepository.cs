using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IExerciseRepository
{
    Task AddExerciseAsync(Exercise exercise);
}