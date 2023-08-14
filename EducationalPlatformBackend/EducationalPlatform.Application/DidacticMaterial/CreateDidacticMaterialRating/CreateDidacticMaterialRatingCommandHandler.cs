using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRating;

public class CreateDidacticMaterialRatingCommandHandler : IRequestHandler<CreateDidacticMaterialRatingCommand,
    OneOf<Success<RatingDto>, BadRequestResult>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;
    private readonly IUserRepository _userRepository;

    public CreateDidacticMaterialRatingCommandHandler(IDidacticMaterialRepository didacticMaterialRepository,
        IUserRepository userRepository)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
        _userRepository = userRepository;
    }

    public async Task<OneOf<Success<RatingDto>, BadRequestResult>> Handle(CreateDidacticMaterialRatingCommand request,
        CancellationToken cancellationToken)
    {
        var didacticMaterialResult =
            await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);

        if (!didacticMaterialResult.TryPickT0(out var didacticMaterial, out _))
            return new BadRequestResult(DidacticMaterialErrorMessages.MaterialWithIdNotExists);

        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);
        if (userResult.IsT1)
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        var result = didacticMaterial.AddNewRating(request.Rating, request.UserId);
        if (!result.TryPickT0(out var success, out var badRequest))
            return badRequest;

        return new Success<RatingDto>(new RatingDto(success.Value, didacticMaterial.GetLastRatings(5)));
    }
}