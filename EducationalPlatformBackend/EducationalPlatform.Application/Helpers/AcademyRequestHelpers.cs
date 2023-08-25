using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.Helpers;

public static class AcademyRequestHelpers
{
    public static IEnumerable<GroupedCreateAcademyEntityRequestDto> GetGroupedCreateAcademyEntityRequests(
        this IEnumerable<CreateAcademyEntityRequest> requests)
    {
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
            .Select(r => new GroupedCreateAcademyEntityRequestDto(r.Key, r));
    }
}