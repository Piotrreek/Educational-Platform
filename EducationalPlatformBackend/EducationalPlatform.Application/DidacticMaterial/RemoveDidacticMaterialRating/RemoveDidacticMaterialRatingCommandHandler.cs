using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.RemoveDidacticMaterialRating;

public class RemoveDidacticMaterialRatingCommandHandler :
    RatingHandler<Domain.Entities.DidacticMaterial, DidacticMaterialRating>,
    IRequestHandler<RemoveDidacticMaterialRatingCommand,
        OneOf<Success<RatingDto>, BadRequestResult>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;

    public RemoveDidacticMaterialRatingCommandHandler(IDidacticMaterialRepository didacticMaterialRepository,
        IUserRepository userRepository) : base(userRepository)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
    }

    public async Task<OneOf<Success<RatingDto>, BadRequestResult>> Handle(
        RemoveDidacticMaterialRatingCommand request, CancellationToken cancellationToken)
    {
        var didacticMaterialResult =
            await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);

        if (!didacticMaterialResult.TryPickT0(out var didacticMaterial, out _))
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.MaterialWithIdNotExists);
        }

        return await RemoveRating(didacticMaterial, request.UserId);
    }
}