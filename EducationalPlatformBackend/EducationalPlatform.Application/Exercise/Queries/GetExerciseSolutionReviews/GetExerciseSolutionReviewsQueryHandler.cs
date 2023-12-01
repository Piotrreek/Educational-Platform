using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Extensions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionReviews;

internal sealed class GetExerciseSolutionReviewsQueryHandler : IRequestHandler<GetExerciseSolutionReviewsQuery,
    OneOf<IEnumerable<ExerciseSolutionReviewDto>, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseSolutionReviewsQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<IEnumerable<ExerciseSolutionReviewDto>, BadRequestResult>> Handle(
        GetExerciseSolutionReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var solutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.ExerciseSolutionId);

        if (!solutionResult.TryPickT0(out var solution, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseSolutionWithIdNotExists);
        }

        return new List<ExerciseSolutionReviewDto>(solution.GetReviews());
    }
}