using EducationalPlatform.Application.Constants;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.CreateAcademyEntityRequest;

public class CreateAcademyEntityRequestCommandHandler : IRequestHandler<CreateAcademyEntityRequestCommand,
    OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAcademyRepository _academyRepository;
    private readonly IGeneralRepository _generalRepository;

    private static readonly Type UniversityType = typeof(Domain.Entities.University);
    private static readonly Type FacultyType = typeof(Domain.Entities.Faculty);
    private static readonly Type UniversitySubjectType = typeof(Domain.Entities.UniversitySubject);
    private static readonly Type UniversityCourseType = typeof(Domain.Entities.UniversityCourse);

    public static readonly string[] ValidTypeNames =
    {
        UniversityType.Name,
        FacultyType.Name,
        UniversitySubjectType.Name,
        UniversityCourseType.Name
    };

    public CreateAcademyEntityRequestCommandHandler(IUserRepository userRepository,
        IAcademyRepository academyRepository, IGeneralRepository generalRepository)
    {
        _userRepository = userRepository;
        _academyRepository = academyRepository;
        _generalRepository = generalRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateAcademyEntityRequestCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.RequesterId);

        if (!userResult.TryPickT0(out var user, out _))
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        if (!ValidTypeNames.Contains(request.EntityType))
        {
            return new BadRequestResult(ValidationErrorMessages.WrongType);
        }

        var createEntityRequest = new Domain.Entities.CreateAcademyEntityRequest(request.EntityName,
            user, request.AdditionalInformation);

        await _academyRepository.CreateAcademyEntityRequestAsync(createEntityRequest);

        if (request.EntityType == FacultyType.Name &&
            !(await TryHandleFacultyRequest(createEntityRequest, request.UniversityId))
                .TryPickT0(out _, out var badRequest))
        {
            _generalRepository.RollbackChanges();
            return badRequest;
        }

        if (request.EntityType == UniversitySubjectType.Name &&
            !(await TryHandleUniversitySubjectRequest(createEntityRequest, request.UniversityId, request.FacultyId,
                request.UniversitySubjectDegree)).TryPickT0(out _, out var subjectBadRequest))
        {
            _generalRepository.RollbackChanges();
            return subjectBadRequest;
        }

        if (request.EntityType == UniversityCourseType.Name && !(await TryHandleUniversityCourseRequest(
                    createEntityRequest,
                    request.UniversityId, request.FacultyId, request.UniversitySubjectId,
                    request.UniversityCourseSession))
                .TryPickT0(out _, out var courseBadRequest))
        {
            _generalRepository.RollbackChanges();
            return courseBadRequest;
        }

        return new Success();
    }

    private async Task<OneOf<Success, BadRequestResult>> TryHandleUniversityCourseRequest(
        Domain.Entities.CreateAcademyEntityRequest createAcademyEntityRequest,
        Guid? universityId, Guid? facultyId, Guid? universitySubjectId, string? universityCourseSession)
    {
        if (!universityId.HasValue)
        {
            return new BadRequestResult(UniversityErrorMessages.IdCannotBeEmpty);
        }

        if (!facultyId.HasValue)
        {
            return new BadRequestResult(FacultyErrorMessages.IdCannotBeEmpty);
        }

        if (!universitySubjectId.HasValue)
        {
            return new BadRequestResult(UniversitySubjectErrorMessages.IdCannotBeEmpty);
        }

        if (!Enum.TryParse<UniversityCourseSession>(universityCourseSession, out var courseSession))
        {
            return new BadRequestResult(GeneralErrorMessages.WrongUniversityCourseSessionConversion);
        }

        var universityResult = await _academyRepository.GetUniversityByIdAsync(universityId);

        if (!universityResult.TryPickT0(out var university, out _))
        {
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);
        }

        var facultyResult = await _academyRepository.GetFacultyByIdAsync(facultyId);

        if (!facultyResult.TryPickT0(out var faculty, out _))
        {
            return new BadRequestResult(FacultyErrorMessages.FacultyWithIdNotExists);
        }

        var universitySubjectResult = await _academyRepository.GetUniversitySubjectByIdAsync(universitySubjectId);

        if (!universitySubjectResult.TryPickT0(out var universitySubject, out _))
        {
            return new BadRequestResult(UniversitySubjectErrorMessages.UniversitySubjectWithIdNotExists);
        }

        createAcademyEntityRequest.AssignPropertiesFoUniversityCourseRequest(university, faculty, universitySubject,
            courseSession);

        return new Success();
    }

    private async Task<OneOf<Success, BadRequestResult>> TryHandleUniversitySubjectRequest(
        Domain.Entities.CreateAcademyEntityRequest createAcademyEntityRequest, Guid? universityId, Guid? facultyId,
        string? universitySubjectDegree)
    {
        if (!universityId.HasValue)
        {
            return new BadRequestResult(UniversityErrorMessages.IdCannotBeEmpty);
        }

        if (!facultyId.HasValue)
        {
            return new BadRequestResult(FacultyErrorMessages.IdCannotBeEmpty);
        }

        if (!Enum.TryParse<UniversitySubjectDegree>(universitySubjectDegree, out var subjectDegree))
        {
            return new BadRequestResult(GeneralErrorMessages.WrongUniversitySubjectDegreeConversion);
        }

        var universityResult = await _academyRepository.GetUniversityByIdAsync(universityId);

        if (!universityResult.TryPickT0(out var university, out _))
        {
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);
        }

        var facultyResult = await _academyRepository.GetFacultyByIdAsync(facultyId);

        if (!facultyResult.TryPickT0(out var faculty, out _))
        {
            return new BadRequestResult(FacultyErrorMessages.FacultyWithIdNotExists);
        }

        createAcademyEntityRequest.AssignPropertiesForUniversitySubjectRequest(university, faculty, subjectDegree);

        return new Success();
    }

    private async Task<OneOf<Success, BadRequestResult>> TryHandleFacultyRequest(
        Domain.Entities.CreateAcademyEntityRequest createAcademyEntityRequest, Guid? universityId)
    {
        if (!universityId.HasValue)
        {
            return new BadRequestResult(UniversityErrorMessages.IdCannotBeEmpty);
        }

        var universityResult = await _academyRepository.GetUniversityByIdAsync(universityId);

        if (!universityResult.TryPickT0(out var university, out _))
        {
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);
        }

        createAcademyEntityRequest.AssignPropertiesForFacultyRequest(university);

        return new Success();
    }
}