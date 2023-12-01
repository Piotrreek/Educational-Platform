using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Extensions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.Exercise.GetExerciseSolutions;

internal sealed class
    GetExerciseSolutionsQueryHandler : IRequestHandler<GetExerciseSolutionsQuery, IEnumerable<ExerciseSolutionDto>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseSolutionsQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<IEnumerable<ExerciseSolutionDto>> Handle(GetExerciseSolutionsQuery request,
        CancellationToken cancellationToken)
    {
        var exerciseSolutions = await _exerciseRepository.GetExerciseSolutionsAsync(request.ExerciseId);

        return exerciseSolutions.Select(exerciseSolution =>
        {
            exerciseSolution.TryGetDidacticMaterialRating(request.UserId, out var usersRating);

            return new ExerciseSolutionDto(exerciseSolution.Id, exerciseSolution.Author.UserName,
                exerciseSolution.CreatedOn.DateTime, exerciseSolution.AverageRating, usersRating?.Rating ?? 0,
                exerciseSolution.GetReviews()
            );
        });
    }
}