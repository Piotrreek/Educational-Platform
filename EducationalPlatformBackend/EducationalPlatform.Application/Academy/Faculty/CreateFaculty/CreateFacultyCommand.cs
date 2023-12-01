using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Faculty.CreateFaculty;

public record CreateFacultyCommand(
    string FacultyName,
    Guid UniversityId
) : IRequest<OneOf<Success, BadRequestResult>>;