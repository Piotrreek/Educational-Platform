using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Models;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionFile;

internal sealed class
    GetExerciseSolutionFileQueryHandler : IRequestHandler<GetExerciseSolutionFileQuery, OneOf<NotFound, BlobDto>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public GetExerciseSolutionFileQueryHandler(IExerciseRepository exerciseRepository,
        IAzureBlobStorageService azureBlobStorageService)
    {
        _exerciseRepository = exerciseRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<OneOf<NotFound, BlobDto>> Handle(GetExerciseSolutionFileQuery request,
        CancellationToken cancellationToken)
    {
        var solutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.Id);

        if (!solutionResult.TryPickT0(out var solution, out _))
        {
            return new NotFound();
        }

        var blobDto = await _azureBlobStorageService.GetBlobByNameAsync(solution.Id);
        blobDto.FileName = solution.FileName;

        return blobDto;
    }
}