using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.Exercise.GetExercises;

public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, IEnumerable<BaseExerciseDto>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExercisesQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<IEnumerable<BaseExerciseDto>> Handle(GetExercisesQuery request,
        CancellationToken cancellationToken)
    {
        var exercises = await _exerciseRepository.GetExercisesByNameAsync(request.Name);

        return exercises.Select(e => new BaseExerciseDto(e.Id, e.Name, e.Author.UserName, e.AverageRating));
    }
}