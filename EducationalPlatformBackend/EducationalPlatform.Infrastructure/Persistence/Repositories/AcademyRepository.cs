using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

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

    public async Task<OneOf<University, NotFound>> GetUniversityByNameAsync(string universityName)
    {
        var university = await _context.Universities
            .SingleOrDefaultAsync(u => u.Name == universityName);

        return OneOfExtensions.GetValueOrNotFoundResult(university);
    }

    public async Task<OneOf<University, NotFound>> GetUniversityByIdAsync(Guid? universityId)
    {
        if (!universityId.HasValue)
            return new NotFound();

        var university = await _context.Universities
            .Include(u => u.Faculties)
            .ThenInclude(f => f.UniversitySubjects)
            .ThenInclude(s => s.UniversityCourses)
            .SingleOrDefaultAsync(u => u.Id == universityId);

        return OneOfExtensions.GetValueOrNotFoundResult(university);
    }
}