using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterials;

public class
    GetDidacticMaterialsQueryHandler : IRequestHandler<GetDidacticMaterialsQuery, IEnumerable<DidacticMaterialDto>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;

    public GetDidacticMaterialsQueryHandler(IDidacticMaterialRepository didacticMaterialRepository)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
    }

    public async Task<IEnumerable<DidacticMaterialDto>> Handle(GetDidacticMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        var didacticMaterials = await _didacticMaterialRepository.GetDidacticMaterials(request.UniversityId,
            request.FacultyId, request.UniversitySubjectId, request.UniversityCourseId);

        return didacticMaterials.Select(material =>
            new DidacticMaterialDto(material.Id, material.Name, material.Author.UserName, material.AverageRating));
    }
}