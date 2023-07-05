using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

public class AcademyRepository : IAcademyRepository
{
    private readonly EducationalPlatformDbContext _context;

    public AcademyRepository(EducationalPlatformDbContext context)
    {
        _context = context;
    }

    public async Task CreateUniversityAsync(string universityName)
    {
        await _context.Universities.AddAsync(new University(universityName));
    }

    public async Task<University?> GetUniversityByNameAsync(string universityName) =>
        await _context.Universities
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Name == universityName);
}