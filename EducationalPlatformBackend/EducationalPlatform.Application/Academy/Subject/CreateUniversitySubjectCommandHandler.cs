using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Subject;

public class CreateUniversitySubjectCommandHandler : IRequestHandler<CreateUniversitySubjectCommand,
    OneOf<Success, BadRequestResult>>
{
    private readonly IAcademyRepository _academyRepository;

    public CreateUniversitySubjectCommandHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateUniversitySubjectCommand request,
        CancellationToken cancellationToken)
    {
        if (!(Enum.TryParse<UniversitySubjectDegree>(request.SubjectDegree, out var universitySubjectDegree) &&
              Enum.IsDefined(universitySubjectDegree)))
            return new BadRequestResult(GeneralErrorMessages.WrongUniversitySubjectDegreeConversion);

        var universityResult = await _academyRepository.GetUniversityByIdAsync(request.UniversityId);
        if (universityResult.IsT1)
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);

        var facultyResult = universityResult.AsT0.GetFacultyById(request.FacultyId);
        if (facultyResult.IsT1)
            return new BadRequestResult(FacultyErrorMessages.FacultyInUniversityNotExists);

        var faculty = facultyResult.AsT0;
        var addSubjectResult = faculty.AddNewSubject(request.SubjectName, universitySubjectDegree);
        if (addSubjectResult.IsT1)
            return addSubjectResult.AsT1;

        return new Success();
    }
}