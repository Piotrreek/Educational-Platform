using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Course;

public record CreateUniversityCourseCommand(
    string CourseName,
    string CourseSession,
    Guid SubjectId
) : IRequest<OneOf<Success, BadRequestResult>>;