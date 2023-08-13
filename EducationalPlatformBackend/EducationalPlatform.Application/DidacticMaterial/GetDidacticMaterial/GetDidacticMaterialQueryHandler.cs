using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterial;

public class GetDidacticMaterialQueryHandler : IRequestHandler<GetDidacticMaterialQuery,
    OneOf<DetailedDidacticMaterialDto, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;

    public GetDidacticMaterialQueryHandler(IUserRepository userRepository,
        IDidacticMaterialRepository didacticMaterialRepository)
    {
        _userRepository = userRepository;
        _didacticMaterialRepository = didacticMaterialRepository;
    }

    public async Task<OneOf<DetailedDidacticMaterialDto, NotFound>> Handle(GetDidacticMaterialQuery request,
        CancellationToken cancellationToken)
    {
        var didacticMaterialResult =
            await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);

        if (!didacticMaterialResult.TryPickT0(out var didacticMaterial, out _))
            return new NotFound();

        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);
        didacticMaterial.TryGetDidacticMaterialRating(request.UserId, out var rating);
        var didacticMaterialIsRateable = userResult.IsT0 && rating is null;

        return new DetailedDidacticMaterialDto(didacticMaterial.Id, didacticMaterial.Name,
            didacticMaterial.Author.UserName, didacticMaterial.AverageRating, didacticMaterial.Description,
            didacticMaterial.University.Name, didacticMaterial.Faculty.Name, didacticMaterial.UniversitySubject.Name,
            didacticMaterial.UniversityCourse.Name,
            didacticMaterial.Ratings.OrderByDescending(c => c.CreatedOn).Take(5).Select(s => s.Rating),
            didacticMaterial.Opinions.Select(s =>
                new DidacticMaterialOpinionDto(s.CreatedOn.DateTime, s.Author.UserName, s.Opinion)),
            didacticMaterialIsRateable, rating?.Rating);
    }
}