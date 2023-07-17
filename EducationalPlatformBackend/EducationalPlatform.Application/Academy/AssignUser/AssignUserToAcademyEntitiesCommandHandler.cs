using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Extensions;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AssignUser;

public class AssignUserToAcademyEntitiesCommandHandler : IRequestHandler<AssignUserToAcademyEntitiesCommand,
    OneOf<Success, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAcademyRepository _academyRepository;
    private readonly IGeneralRepository _generalRepository;

    public AssignUserToAcademyEntitiesCommandHandler(IUserRepository userRepository,
        IAcademyRepository academyRepository, IGeneralRepository generalRepository)
    {
        _userRepository = userRepository;
        _academyRepository = academyRepository;
        _generalRepository = generalRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(AssignUserToAcademyEntitiesCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);
        if (userResult.IsT1)
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        var universityResult = await _academyRepository.GetUniversityByIdAsync(request.UniversityId);
        if (universityResult.IsT1)
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);

        var university = universityResult.AsT0;
        var facultyResult = university.GetFacultyById(request.FacultyId);

        if (facultyResult.IsT1)
            return new BadRequestResult(FacultyErrorMessages.FacultyInUniversityNotExists);

        var faculty = facultyResult.AsT0;
        var user = userResult.AsT0;

        user.AssignToUniversity(university);
        var assignToFacultyResult = user.AssignToFaculty(faculty);
        var assignToSubjectResult = user.AssignToUniversitySubject(request.UniversitySubjectId);

        if (!assignToFacultyResult.IsT1 && !assignToSubjectResult.IsT1) return new Success();

        _generalRepository.RollbackChanges();
        return new BadRequestResult(
            $@"""{(assignToFacultyResult.IsT1 ? assignToFacultyResult.AsT1.Message + "." : string.Empty)}
                        {(assignToSubjectResult.IsT1 ? assignToSubjectResult.AsT1.Message + "." : string.Empty)}""");
    }
}