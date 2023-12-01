using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EducationPlatform.Infrastructure.Persistence.Repositories;

internal sealed class AcademyRepository : IAcademyRepository
{
    private readonly DbSet<University> _universities;
    private readonly DbSet<Faculty> _faculties;
    private readonly DbSet<UniversityCourse> _universityCourses;
    private readonly DbSet<UniversitySubject> _universitySubjects;
    private readonly DbSet<CreateAcademyEntityRequest> _createAcademyEntityRequests;

    public AcademyRepository(DbSet<University> universities, DbSet<UniversityCourse> universityCourses,
        DbSet<UniversitySubject> universitySubjects, DbSet<Faculty> faculties,
        DbSet<CreateAcademyEntityRequest> createAcademyEntityRequests)
    {
        _universities = universities;
        _universityCourses = universityCourses;
        _universitySubjects = universitySubjects;
        _faculties = faculties;
        _createAcademyEntityRequests = createAcademyEntityRequests;
    }

    public async Task CreateUniversityAsync(string universityName)
    {
        await _universities.AddAsync(University.Create(universityName));
    }

    public async Task<OneOf<University, NotFound>> GetUniversityByNameAsync(string universityName)
    {
        var university = await _universities
            .SingleOrDefaultAsync(u => u.Name == universityName);

        return OneOfExtensions.GetValueOrNotFoundResult(university);
    }

    public async Task<OneOf<University, NotFound>> GetUniversityByIdAsync(Guid? universityId)
    {
        if (!universityId.HasValue)
            return new NotFound();

        var university = await _universities
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

        var universityCourse = await _universityCourses
            .Include(c => c.DidacticMaterials)
            .ThenInclude(d => d.Opinions)
            .SingleOrDefaultAsync(c => c.Id == universityCourseId);

        return OneOfExtensions.GetValueOrNotFoundResult(universityCourse);
    }

    public async Task<OneOf<UniversitySubject, NotFound>> GetUniversitySubjectByIdAsync(Guid? universitySubjectId)
    {
        if (!universitySubjectId.HasValue)
            return new NotFound();

        var universitySubject = await _universitySubjects
            .Include(s => s.UniversityCourses)
            .SingleOrDefaultAsync(s => s.Id == universitySubjectId);

        return OneOfExtensions.GetValueOrNotFoundResult(universitySubject);
    }

    public async Task<OneOf<Faculty, NotFound>> GetFacultyByIdAsync(Guid? facultyId)
    {
        if (!facultyId.HasValue)
            return new NotFound();

        var faculty = await _faculties
            .Include(f => f.UniversitySubjects)
            .ThenInclude(u => u.UniversityCourses)
            .SingleOrDefaultAsync(f => f.Id == facultyId);

        return OneOfExtensions.GetValueOrNotFoundResult(faculty);
    }

    public async Task<IReadOnlyCollection<University>> GetAllUniversitiesAsync()
    {
        return await _universities
            .AsNoTracking()
            .Include(u => u.Faculties)
            .ThenInclude(f => f.UniversitySubjects)
            .ThenInclude(s => s.UniversityCourses)
            .ToListAsync();
    }

    public async Task CreateAcademyEntityRequestAsync(CreateAcademyEntityRequest request)
    {
        await _createAcademyEntityRequests.AddAsync(request);
    }

    public async Task<IEnumerable<CreateAcademyEntityRequest>> GetNotResolvedRequestsAsync()
    {
        return await _createAcademyEntityRequests
            .Include(c => c.University)
            .Include(c => c.Faculty)
            .Include(c => c.UniversitySubject)
            .Include(c => c.Requester)
            .Where(c => c.Status == CreateAcademyEntityRequestStatus.Created)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<OneOf<CreateAcademyEntityRequest, NotFound>> GetRequestByIdAsync(Guid id)
    {
        return OneOfExtensions.GetValueOrNotFoundResult(
            await _createAcademyEntityRequests.SingleOrDefaultAsync(d => d.Id == id));
    }
}