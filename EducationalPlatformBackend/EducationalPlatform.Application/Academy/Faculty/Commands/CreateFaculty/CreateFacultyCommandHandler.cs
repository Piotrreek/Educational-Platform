using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.Faculty.Commands.CreateFaculty;

internal sealed class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, OneOf<Success, BadRequestResult>>
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
        if (!universityResult.TryPickT0(out var university, out _))
            return new BadRequestResult(UniversityErrorMessages.UniversityWithIdNotExists);
        
        return university.AddNewFaculty(request.FacultyName);
    }
}