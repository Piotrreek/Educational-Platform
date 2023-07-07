using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Course;

public class CreateUniversityCourseCommandHandler : IRequestHandler<CreateUniversityCourseCommand,
    OneOf<Success, BadRequestResult>>
{
    private readonly IAcademyRepository _academyRepository;

    public CreateUniversityCourseCommandHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateUniversityCourseCommand request,
        CancellationToken cancellationToken)
    {
        if (!(Enum.TryParse<UniversityCourseSession>(request.CourseSession, out var universityCourseSession) &&
              Enum.IsDefined(universityCourseSession)))
            return new BadRequestResult(GeneralErrorMessages.WrongUniversityCourseSessionConversion);

        var universityResult = await _academyRepository.GetUniversityByIdAsync(request.UniversityId);
        if (universityResult.IsT1)
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);

        var subjectResult = universityResult.AsT0.GetUniversitySubjectById(request.FacultyId, request.SubjectId);
        if (subjectResult.IsT1)
            return new BadRequestResult(GeneralErrorMessages.WrongFacultyAndSubjectIds);

        var subject = subjectResult.AsT0;
        var addCourseResult = subject.AddNewCourse(request.CourseName, universityCourseSession);
        if (addCourseResult.IsT1)
            return addCourseResult.AsT1;

        return new Success();
    }
}