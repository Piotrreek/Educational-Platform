using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Models;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialFile;

internal sealed class
    GetDidacticMaterialFileQueryHandler : IRequestHandler<GetDidacticMaterialFileQuery, OneOf<NotFound, BlobDto>>
{
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public GetDidacticMaterialFileQueryHandler(IDidacticMaterialRepository didacticMaterialRepository,
        IAzureBlobStorageService azureBlobStorageService)
    {
        _didacticMaterialRepository = didacticMaterialRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<OneOf<NotFound, BlobDto>> Handle(GetDidacticMaterialFileQuery request,
        CancellationToken cancellationToken)
    {
        var didacticMaterialResult =
            await _didacticMaterialRepository.GetDidacticMaterialByIdAsync(request.DidacticMaterialId);
        if (!didacticMaterialResult.TryPickT0(out var didacticMaterial, out _))
            return new NotFound();

        var blobDto = await _azureBlobStorageService.GetBlobByNameAsync(didacticMaterial.Id);
        blobDto.FileName = didacticMaterial.Name;

        return blobDto;
    }
}