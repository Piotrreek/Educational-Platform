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
        if (!userResult.TryPickT0(out var user, out _))
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        Domain.Entities.Faculty? faculty = null;

        (await _academyRepository.GetUniversityByIdAsync(request.UniversityId)).TryPickT0(out var university, out _);
        if (request.UniversityId.HasValue && university is null)
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);
        
        university?.GetFacultyById(request.FacultyId).TryPickT0(out faculty, out _);
        if (request.FacultyId.HasValue && faculty is null)
            return new BadRequestResult(FacultyErrorMessages.FacultyWithIdNotExists);

        user.AssignToUniversity(university);
        var assignToFacultyResult = user.AssignToFaculty(faculty);
        var assignToSubjectResult = user.AssignToUniversitySubject(request.UniversitySubjectId);

        if (!assignToFacultyResult.IsT1 && !assignToSubjectResult.IsT1) return new Success();

        _generalRepository.RollbackChanges();
        return new BadRequestResult(
            $"{(assignToFacultyResult.TryPickT1(out var facultyBadRequest, out _) ? facultyBadRequest.Message + "." : string.Empty)}{(assignToSubjectResult.TryPickT1(out var subjectBadRequest, out _) ? subjectBadRequest.Message + "." : string.Empty)}");
    }
}