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

        var subjectResult = await _academyRepository.GetUniversitySubjectByIdAsync(request.SubjectId);
        if (!subjectResult.TryPickT0(out var subject, out _))
            return new BadRequestResult(UniversitySubjectErrorMessages.UniversitySubjectWithIdNotExists);

        return subject.AddNewCourse(request.CourseName, universityCourseSession);
    }
}