using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExercise;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand,
    OneOf<Success, BadRequestResult, ServiceUnavailableResult>>
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IGeneralRepository _generalRepository;

    public CreateExerciseCommandHandler(IUserRepository userRepository,
        IAzureBlobStorageService azureBlobStorageService, IExerciseRepository exerciseRepository,
        IGeneralRepository generalRepository)
    {
        _userRepository = userRepository;
        _azureBlobStorageService = azureBlobStorageService;
        _exerciseRepository = exerciseRepository;
        _generalRepository = generalRepository;
    }

    public async Task<OneOf<Success, BadRequestResult, ServiceUnavailableResult>> Handle(CreateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.AuthorId);

        if (userResult.IsT1)
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        var exercise = new Domain.Entities.Exercise(request.Name, request.ExerciseFile.FileName, request.AuthorId,
            request.Description);

        await _exerciseRepository.AddExerciseAsync(exercise);

        try
        {
            await _azureBlobStorageService.UploadBlobAsync(exercise.Id, request.ExerciseFile);
        }
        catch (Exception)
        {
            _generalRepository.RollbackChanges();

            return new ServiceUnavailableResult();
        }

        return new Success();
    }
}