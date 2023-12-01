using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Commands.RemoveExerciseSolutionRating;

internal sealed class RemoveExerciseSolutionRatingCommandHandler : RatingHandler<ExerciseSolution, ExerciseSolutionRating>,
    IRequestHandler<RemoveExerciseSolutionRatingCommand, OneOf<Success<RatingDto>, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public RemoveExerciseSolutionRatingCommandHandler(IUserRepository userRepository,
        IExerciseRepository exerciseRepository) : base(userRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<Success<RatingDto>, BadRequestResult>> Handle(RemoveExerciseSolutionRatingCommand request,
        CancellationToken cancellationToken)
    {
        var solutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.SolutionId);

        if (!solutionResult.TryPickT0(out var solution, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseSolutionWithIdNotExists);
        }

        return await RemoveRating(solution, request.UserId);
    }
}