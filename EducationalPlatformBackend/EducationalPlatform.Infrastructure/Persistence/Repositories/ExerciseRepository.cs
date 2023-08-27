using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;

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
}