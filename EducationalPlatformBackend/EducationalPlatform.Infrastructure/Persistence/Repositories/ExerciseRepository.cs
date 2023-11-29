using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

internal sealed class ExerciseRepository : IExerciseRepository
{
    private readonly DbSet<Exercise> _exercises;
    private readonly DbSet<ExerciseSolution> _exerciseSolutions;
    private readonly DbSet<ExerciseSolutionReview> _exerciseSolutionReviews;

    public ExerciseRepository(DbSet<Exercise> exercises, DbSet<ExerciseSolution> exerciseSolutions,
        DbSet<ExerciseSolutionReview> exerciseSolutionReviews)
    {
        _exercises = exercises;
        _exerciseSolutions = exerciseSolutions;
        _exerciseSolutionReviews = exerciseSolutionReviews;
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        await _exercises.AddAsync(exercise);
    }

    public async Task<OneOf<Exercise, NotFound>> GetExerciseByIdAsync(Guid id)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(
            await _exercises
                .Include(c => c.Author)
                .Include(c => c.Solutions)
                .ThenInclude(c => c.Author)
                .Include(c => c.Solutions)
                .ThenInclude(c => c.Ratings)
                .Include(c => c.Solutions)
                .ThenInclude(c => c.Reviews)
                .ThenInclude(c => c.Author)
                .Include(c => c.Ratings)
                .Include(c => c.Comments)
                .ThenInclude(c => c.Author)
                .AsSplitQuery()
                .SingleOrDefaultAsync(c => c.Id == id));
    }

    public async Task<IReadOnlyCollection<Exercise>> GetExercisesByNameAsync(string? name)
    {
        return await _exercises.AsNoTracking()
            .Include(e => e.Author)
            .Include(e => e.Ratings)
            .Where(e => name == null || e.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<OneOf<ExerciseSolution, NotFound>> GetExerciseSolutionByIdAsync(Guid id)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(
            await _exerciseSolutions
                .Include(s => s.Ratings)
                .Include(s => s.Reviews)
                .ThenInclude(c => c.Author)
                .SingleOrDefaultAsync(c => c.Id == id));
    }

    public async Task<IReadOnlyCollection<ExerciseSolution>> GetExerciseSolutionsAsync(Guid exerciseId)
    {
        return await _exerciseSolutions
            .Include(c => c.Author)
            .Include(c => c.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(c => c.ExerciseId == exerciseId)
            .ToListAsync();
    }

    public async Task<OneOf<ExerciseSolutionReview, NotFound>> GetExerciseSolutionReviewByIdAsync(Guid reviewId)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(await _exerciseSolutionReviews
            .SingleOrDefaultAsync(s => s.Id == reviewId));
    }
}