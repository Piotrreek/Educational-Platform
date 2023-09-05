using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolution;

public class CreateExerciseSolutionCommandHandler : IRequestHandler<CreateExerciseSolutionCommand,
    OneOf<Success<IReadOnlyCollection<ExerciseSolutionDto>>, BadRequestResult, ServiceUnavailableResult>>
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IGeneralRepository _generalRepository;

    public CreateExerciseSolutionCommandHandler(IAzureBlobStorageService azureBlobStorageService,
        IUserRepository userRepository, IExerciseRepository exerciseRepository, IGeneralRepository generalRepository)
    {
        _azureBlobStorageService = azureBlobStorageService;
        _userRepository = userRepository;
        _exerciseRepository = exerciseRepository;
        _generalRepository = generalRepository;
    }

    public async Task<OneOf<Success<IReadOnlyCollection<ExerciseSolutionDto>>, BadRequestResult,
            ServiceUnavailableResult>>
        Handle(CreateExerciseSolutionCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.AuthorId);

        if (!userResult.TryPickT0(out var user, out _))
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseWithIdNotExists);
        }

        var result = exercise.AddSolution(request.SolutionFile.FileName, user);

        if (!result.TryPickT0(out var success, out var badRequest))
        {
            return badRequest;
        }

        try
        {
            await _azureBlobStorageService.UploadBlobAsync(success.Value.Item2.Id, request.SolutionFile);
        }
        catch (Exception)
        {
            _generalRepository.RollbackChanges();

            return new ServiceUnavailableResult();
        }

        return new Success<IReadOnlyCollection<ExerciseSolutionDto>>(success.Value.Item1.Select(s =>
            {
                s.TryGetDidacticMaterialRating(request.AuthorId, out var usersRating);

                return new ExerciseSolutionDto(s.Id, s.Author.UserName,
                    s.CreatedOn.Year == 1 ? DateTime.Now : s.CreatedOn.DateTime, s.AverageRating,
                    usersRating?.Rating ?? 0);
            }
        ).ToList());
    }
}