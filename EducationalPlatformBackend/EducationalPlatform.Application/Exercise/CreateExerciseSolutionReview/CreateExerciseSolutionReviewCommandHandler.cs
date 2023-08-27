using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolutionReview;

public class
    CreateExerciseSolutionReviewCommandHandler : IRequestHandler<CreateExerciseSolutionReviewCommand,
        OneOf<Success, BadRequestResult, ServiceUnavailableResult>>
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IGeneralRepository _generalRepository;

    public CreateExerciseSolutionReviewCommandHandler(IUserRepository userRepository,
        IExerciseRepository exerciseRepository, IAzureBlobStorageService azureBlobStorageService,
        IGeneralRepository generalRepository)
    {
        _userRepository = userRepository;
        _exerciseRepository = exerciseRepository;
        _azureBlobStorageService = azureBlobStorageService;
        _generalRepository = generalRepository;
    }

    public async Task<OneOf<Success, BadRequestResult, ServiceUnavailableResult>> Handle(
        CreateExerciseSolutionReviewCommand request,
        CancellationToken cancellationToken)
    {
        if (request.ReviewContent is null && request.ReviewFile is null)
        {
            return new BadRequestResult("Both content and file cannot be empty!");
        }

        var userResult = await _userRepository.GetUserByIdAsync(request.AuthorId);

        if (userResult.IsT1)
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        var exerciseSolutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.ExerciseSolutionId);

        if (!exerciseSolutionResult.TryPickT0(out var exerciseSolution, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseSolutionWithIdNotExists);
        }

        var reviewId =
            exerciseSolution.AddReview(request.AuthorId, request.ReviewContent, request.ReviewFile?.FileName);

        if (request.ReviewFile is null)
        {
            return new Success();
        }

        try
        {
            await _azureBlobStorageService.UploadBlobAsync(reviewId, request.ReviewFile);
        }
        catch (Exception)
        {
            _generalRepository.RollbackChanges();

            return new ServiceUnavailableResult();
        }

        return new Success();
    }
}