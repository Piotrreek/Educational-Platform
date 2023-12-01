using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Models;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.GetExerciseFile;

internal sealed class GetExerciseFileQueryHandler : IRequestHandler<GetExerciseFileQuery, OneOf<NotFound, BlobDto>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public GetExerciseFileQueryHandler(IExerciseRepository exerciseRepository,
        IAzureBlobStorageService azureBlobStorageService)
    {
        _exerciseRepository = exerciseRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<OneOf<NotFound, BlobDto>> Handle(GetExerciseFileQuery request,
        CancellationToken cancellationToken)
    {
        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.Id);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new NotFound();
        }

        var blobDto = await _azureBlobStorageService.GetBlobByNameAsync(exercise.Id);
        blobDto.FileName = exercise.FileName;

        return blobDto;
    }
}