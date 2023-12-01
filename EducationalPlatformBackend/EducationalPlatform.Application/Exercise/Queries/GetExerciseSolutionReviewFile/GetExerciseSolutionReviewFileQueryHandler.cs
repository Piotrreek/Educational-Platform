using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Models;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionReviewFile;

internal sealed class
    GetExerciseSolutionReviewFileQueryHandler : IRequestHandler<GetExerciseSolutionReviewFileQuery,
        OneOf<Success<BlobDto>, NotFound, ServiceUnavailableResult>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public GetExerciseSolutionReviewFileQueryHandler(IExerciseRepository exerciseRepository,
        IAzureBlobStorageService azureBlobStorageService)
    {
        _exerciseRepository = exerciseRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<OneOf<Success<BlobDto>, NotFound, ServiceUnavailableResult>> Handle(
        GetExerciseSolutionReviewFileQuery request,
        CancellationToken cancellationToken)
    {
        var reviewResult = await _exerciseRepository.GetExerciseSolutionReviewByIdAsync(request.ReviewId);

        if (!reviewResult.TryPickT0(out var review, out var notFound))
        {
            return notFound;
        }

        try
        {
            var blobDto = await _azureBlobStorageService.GetBlobByNameAsync(review.Id);
            blobDto.FileName = review.FileName!;

            return new Success<BlobDto>(blobDto);
        }
        catch (Exception)
        {
            return new ServiceUnavailableResult();
        }
    }
}