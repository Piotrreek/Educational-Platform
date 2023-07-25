using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Subject;

public record CreateUniversitySubjectCommand
    (string SubjectName, string SubjectDegree, Guid FacultyId) : IRequest<OneOf<Success, BadRequestResult>>;