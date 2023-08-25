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

    public async Task<OneOf<UniversityCourse, NotFound>> GetUniversityCourseByIdAsync(Guid? universityCourseId)
    {
        if (!universityCourseId.HasValue)
            return new NotFound();

        var universityCourse = await _context.UniversityCourses
            .Include(c => c.DidacticMaterials)
            .ThenInclude(d => d.Opinions)
            .SingleOrDefaultAsync(c => c.Id == universityCourseId);

        return OneOfExtensions.GetValueOrNotFoundResult(universityCourse);
    }

    public async Task<OneOf<UniversitySubject, NotFound>> GetUniversitySubjectByIdAsync(Guid? universitySubjectId)
    {
        if (!universitySubjectId.HasValue)
            return new NotFound();

        var universitySubject = await _context.UniversitySubjects
            .Include(s => s.UniversityCourses)
            .SingleOrDefaultAsync(s => s.Id == universitySubjectId);

        return OneOfExtensions.GetValueOrNotFoundResult(universitySubject);
    }

    public async Task<OneOf<Faculty, NotFound>> GetFacultyByIdAsync(Guid? facultyId)
    {
        if (!facultyId.HasValue)
            return new NotFound();

        var faculty = await _context.Faculties
            .Include(f => f.UniversitySubjects)
            .ThenInclude(u => u.UniversityCourses)
            .SingleOrDefaultAsync(f => f.Id == facultyId);

        return OneOfExtensions.GetValueOrNotFoundResult(faculty);
    }

    public async Task<IReadOnlyCollection<University>> GetAllUniversitiesAsync()
    {
        return await _context.Universities
            .AsNoTracking()
            .Include(u => u.Faculties)
            .ThenInclude(f => f.UniversitySubjects)
            .ThenInclude(s => s.UniversityCourses)
            .ToListAsync();
    }

    public async Task CreateAcademyEntityRequestAsync(CreateAcademyEntityRequest request)
    {
        await _context.CreateAcademyEntityRequests.AddAsync(request);
    }

    public async Task<IEnumerable<CreateAcademyEntityRequest>> GetNotResolvedRequestsAsync()
    {
        return await _context.CreateAcademyEntityRequests
            .Include(c => c.University)
            .Include(c => c.Faculty)
            .Include(c => c.UniversitySubject)
            .Include(c => c.Requester)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<OneOf<CreateAcademyEntityRequest, NotFound>> GetRequestByIdAsync(Guid id)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(
            await _context.CreateAcademyEntityRequests.SingleOrDefaultAsync(d => d.Id == id));
    }
}