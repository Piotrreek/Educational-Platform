using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly EducationalPlatformDbContext _context;

    public ExerciseRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        await _context.Exercises.AddAsync(exercise);
    }

    public async Task<OneOf<Exercise, NotFound>> GetExerciseByIdAsync(Guid id)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(await _context.Exercises.SingleOrDefaultAsync(c => c.Id == id));
    }
}