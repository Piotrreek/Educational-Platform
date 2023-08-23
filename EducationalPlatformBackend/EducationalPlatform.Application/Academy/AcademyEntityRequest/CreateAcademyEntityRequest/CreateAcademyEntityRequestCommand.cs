using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.CreateAcademyEntityRequest;

public record CreateAcademyEntityRequestCommand(string EntityType, string EntityName,
        string? AdditionalInformation, string? UniversitySubjectDegree, string? UniversityCourseSession,
        Guid? UniversityId, Guid? FacultyId, Guid? UniversitySubjectId, Guid RequesterId)
    : IRequest<OneOf<Success, BadRequestResult>>;