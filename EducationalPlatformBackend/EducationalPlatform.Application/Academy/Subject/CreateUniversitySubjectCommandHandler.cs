using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Subject;

internal sealed class CreateUniversitySubjectCommandHandler : IRequestHandler<CreateUniversitySubjectCommand,
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

        var facultyResult = await _academyRepository.GetFacultyByIdAsync(request.FacultyId);
        if (!facultyResult.TryPickT0(out var faculty, out _))
            return new BadRequestResult(FacultyErrorMessages.FacultyWithIdNotExists);

        return faculty.AddNewSubject(request.SubjectName, universitySubjectDegree);
    }
}