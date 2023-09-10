using EducationalPlatform.Domain.Entities;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Abstractions.Repositories;

public interface IExerciseRepository
{
    Task AddExerciseAsync(Exercise exercise);
    Task<OneOf<Exercise, NotFound>> GetExerciseByIdAsync(Guid id);
    Task<IReadOnlyCollection<Exercise>> GetExercisesByNameAsync(string? name);
    Task<OneOf<ExerciseSolution, NotFound>> GetExerciseSolutionByIdAsync(Guid id);
    Task<IReadOnlyCollection<ExerciseSolution>> GetExerciseSolutionsAsync(Guid exerciseId);
    Task<OneOf<ExerciseSolutionReview, NotFound>> GetExerciseSolutionReviewByIdAsync(Guid reviewId);
}