using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Faculty.CreateFaculty;

public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IAcademyRepository _academyRepository;

    public CreateFacultyCommandHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var universityResult = await _academyRepository.GetUniversityByIdAsync(request.UniversityId);
        if (universityResult.IsT1)
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);

        var university = universityResult.AsT0;
        var addFacultyResult = university.AddNewFaculty(request.FacultyName);
        if (addFacultyResult.IsT1)
            return addFacultyResult.AsT1;

        return new Success();
    }
}