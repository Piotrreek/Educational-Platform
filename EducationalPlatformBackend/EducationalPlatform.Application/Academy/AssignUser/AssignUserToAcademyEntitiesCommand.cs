using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Application.Academy.AssignUser;

public record AssignUserToAcademyEntitiesCommand(Guid UserId, Guid? UniversityId, Guid? FacultyId,
    Guid? UniversitySubjectId) : IRequest<OneOf<Success, BadRequestResult>>;