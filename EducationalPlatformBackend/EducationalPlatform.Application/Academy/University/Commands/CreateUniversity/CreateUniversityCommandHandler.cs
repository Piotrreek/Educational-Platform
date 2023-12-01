using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.University.Commands.CreateUniversity;

internal sealed class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IAcademyRepository _academyRepository;

    public CreateUniversityCommandHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateUniversityCommand request,
        CancellationToken cancellationToken)
    {
        if (await UniversityWithNameAlreadyExists(request.UniversityName))
            return new BadRequestResult(UniversityErrorMessages.UniversityWithNameAlreadyExists);

        await _academyRepository.CreateUniversityAsync(request.UniversityName);

        return new Success();
    }

    private async Task<bool> UniversityWithNameAlreadyExists(string name)
        => (await _academyRepository.GetUniversityByNameAsync(name)).IsT0;
}