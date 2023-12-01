using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.Commands.CreateDidacticMaterialOpinion;

internal sealed class CreateDidacticMaterialOpinionCommandHandler : IRequestHandler<CreateDidacticMaterialOpinionCommand,
    OneOf<Success<IEnumerable<OpinionDto>>, BadRequestResult>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;
    private readonly IUserRepository _userRepository;

    public CreateDidacticMaterialOpinionCommandHandler(IDidacticMaterialRepository didacticMaterialRepository,
        IUserRepository userRepository)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
        _userRepository = userRepository;
    }

    public async Task<OneOf<Success<IEnumerable<OpinionDto>>, BadRequestResult>> Handle(
        CreateDidacticMaterialOpinionCommand request,
        CancellationToken cancellationToken)
    {
        var didacticMaterialResult =
            await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);

        if (!didacticMaterialResult.TryPickT0(out var didacticMaterial, out _))
            return new BadRequestResult(DidacticMaterialErrorMessages.MaterialWithIdNotExists);

        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);

        if (!userResult.TryPickT0(out _, out _))
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        var result = didacticMaterial.AddOpinion(request.Opinion, request.UserId);
        if (!result.TryPickT0(out var opinions, out var badRequest))
            return badRequest;

        return new Success<IEnumerable<OpinionDto>>(opinions.Select(s =>
            new OpinionDto(s.CreatedOn.DateTime, s.Author.UserName, s.Opinion)));
    }
}