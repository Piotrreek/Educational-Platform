using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.University.CreateUniversity;

public class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IAcademyRepository _academyRepository;

    public CreateUniversityCommandHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateUniversityCommand request,
        CancellationToken cancellationToken)
    {
        if (await UniversityWithNameExists(request.UniversityName))
            return new BadRequestResult(UniversityErrorMessages.UniversityWithNameAlreadyExists);

        await _academyRepository.CreateUniversityAsync(request.UniversityName);

        return new Success();
    }

    private async Task<bool> UniversityWithNameExists(string name)
        => await _academyRepository.GetUniversityByNameAsync(name) is not null;
}