using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialOpinions;

internal sealed class GetDidacticMaterialOpinionsQueryHandler : IRequestHandler<GetDidacticMaterialOpinionsQuery,
    OneOf<IEnumerable<OpinionDto>, BadRequestResult>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;

    public GetDidacticMaterialOpinionsQueryHandler(IDidacticMaterialRepository didacticMaterialRepository)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
    }

    public async Task<OneOf<IEnumerable<OpinionDto>, BadRequestResult>> Handle(GetDidacticMaterialOpinionsQuery request,
        CancellationToken cancellationToken)
    {
        var materialResult = await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);

        if (!materialResult.TryPickT0(out var material, out _))
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.MaterialWithIdNotExists);
        }

        return material.Opinions.Select(c => new OpinionDto(c.CreatedOn.DateTime, c.Author.UserName, c.Opinion))
            .ToList();
    }
}