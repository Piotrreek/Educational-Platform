using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.GetCreateAcademyEntityRequests;

public class GetCreateAcademyEntityRequestsQueryHandler : IRequestHandler<GetCreateAcademyEntityRequestsQuery,
    IEnumerable<GetGroupedCreateAcademyEntityRequestDto>>
{
    private readonly IAcademyRepository _academyRepository;

    public GetCreateAcademyEntityRequestsQueryHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<IEnumerable<GetGroupedCreateAcademyEntityRequestDto>> Handle(
        GetCreateAcademyEntityRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var requests = await _academyRepository.GetRequestsToCreateEntities();

        return requests.Select(r => new GetCreateAcademyEntityRequestDto(r.EntityTypeName,
                r.EntityName, r.Status.ToString(), r.RequesterId, r.Requester.UserName, r.Id)
            {
                AdditionalInformation = r.AdditionalInformation,
                SubjectDegree = r.UniversitySubjectDegree.ToString(),
                CourseSession = r.UniversityCourseSession.ToString(),
                UniversityId = r.UniversityId,
                UniversityName = r.University?.Name,
                FacultyId = r.FacultyId,
                FacultyName = r.Faculty?.Name,
                SubjectId = r.UniversitySubjectId,
                SubjectName = r.UniversitySubject?.Name
            })
            .GroupBy(r => r.EntityType)
            .Select(r => new GetGroupedCreateAcademyEntityRequestDto(r.Key, r));
    }
}