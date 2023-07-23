using EducationalPlatform.Application.Contracts.Academy.Faculty;
using EducationalPlatform.Application.Contracts.Academy.University;
using EducationalPlatform.Application.Contracts.Academy.UniversityCourse;
using EducationalPlatform.Application.Contracts.Academy.UniversitySubject;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.Academy.GetGroupedAcademyEntities;

public class GetGroupedAcademyEntitiesQueryHandler : IRequestHandler<GetGroupedAcademyEntitiesQuery, IEnumerable<UniversityDto>>
{
    private readonly IAcademyRepository _academyRepository;

    public GetGroupedAcademyEntitiesQueryHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<IEnumerable<UniversityDto>> Handle(GetGroupedAcademyEntitiesQuery request,
        CancellationToken cancellationToken)
    {
        var universities = (await _academyRepository.GetAllUniversitiesAsync())
            .Select(university => new UniversityDto
            {
                Id = university.Id,
                Name = university.Name,
                Faculties = university.Faculties.Select(faculty => new FacultyDto
                {
                    Id = faculty.Id,
                    Name = faculty.Name,
                    UniversitySubjects = faculty.UniversitySubjects.Select(universitySubject => new UniversitySubjectDto
                    {
                        Id = universitySubject.Id,
                        Name = universitySubject.Name,
                        UniversitySubjectDegree = universitySubject.UniversitySubjectDegree.ToString(),
                        UniversityCourses = universitySubject.UniversityCourses.Select(universityCourse =>
                            new UniversityCourseDto
                            {
                                Id = universityCourse.Id,
                                Name = universityCourse.Name,
                                UniversityCourseSession = universityCourse.UniversityCourseSession.ToString()
                            })
                    })
                })
            });

        return universities;
    }
}