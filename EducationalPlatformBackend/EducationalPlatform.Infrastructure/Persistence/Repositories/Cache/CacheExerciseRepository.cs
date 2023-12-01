using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories.Cache;

internal sealed class CacheExerciseRepository : IExerciseRepository
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMemoryCache _cache;

    public CacheExerciseRepository(IExerciseRepository exerciseRepository, IMemoryCache cache)
    {
        _exerciseRepository = exerciseRepository;
        _cache = cache;
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        await _exerciseRepository.AddExerciseAsync(exercise);
    }

    public async Task<OneOf<Exercise, NotFound>> GetExerciseByIdAsync(Guid id)
    {
        return await _exerciseRepository.GetExerciseByIdAsync(id);
    }

    public async Task<IReadOnlyCollection<Exercise>> GetExercisesByNameAsync(string? name)
    {
        var cacheKey = $"exercises-${name}";
        return await _cache.GetOrSaveAndGet(cacheKey, () => _exerciseRepository.GetExercisesByNameAsync(name));
    }

    public async Task<OneOf<ExerciseSolution, NotFound>> GetExerciseSolutionByIdAsync(Guid id)
    {
        return await _exerciseRepository.GetExerciseSolutionByIdAsync(id);
    }

    public async Task<IReadOnlyCollection<ExerciseSolution>> GetExerciseSolutionsAsync(Guid exerciseId)
    {
        var cacheKey = $"exercise-solutions-${exerciseId}";
        return await _cache.GetOrSaveAndGet(cacheKey, () => _exerciseRepository.GetExerciseSolutionsAsync(exerciseId));
    }

    public async Task<OneOf<ExerciseSolutionReview, NotFound>> GetExerciseSolutionReviewByIdAsync(Guid reviewId)
    {
        var cacheKey = $"exercise-solution-review-${reviewId}";
        return await _cache.GetOrSaveAndGet(cacheKey,
            () => _exerciseRepository.GetExerciseSolutionReviewByIdAsync(reviewId));
    }
}