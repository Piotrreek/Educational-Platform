using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.GetExerciseSolutionRating;

public class GetExerciseSolutionRatingQueryHandler : IRequestHandler<GetExerciseSolutionRatingQuery,
    OneOf<SolutionRatingDto, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseSolutionRatingQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<SolutionRatingDto, BadRequestResult>> Handle(GetExerciseSolutionRatingQuery request,
        CancellationToken cancellationToken)
    {
        var exerciseSolutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.ExerciseSolutionId);

        if (!exerciseSolutionResult.TryPickT0(out var exerciseSolution, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseSolutionWithIdNotExists);
        }

        exerciseSolution.TryGetDidacticMaterialRating(request.UserId, out var usersRating);

        return new SolutionRatingDto(exerciseSolution.AverageRating, usersRating?.Rating ?? 0);
    }
}