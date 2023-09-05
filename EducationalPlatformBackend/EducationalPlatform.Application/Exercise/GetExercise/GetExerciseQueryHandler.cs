using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.GetExercise;

public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, OneOf<Success<ExerciseDto>, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseQueryHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
    {
        _exerciseRepository = exerciseRepository;
        _userRepository = userRepository;
    }

    public async Task<OneOf<Success<ExerciseDto>, NotFound>> Handle(GetExerciseQuery request,
        CancellationToken cancellationToken)
    {
        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.Id);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new NotFound();
        }

        var userResult = await _userRepository.GetUserByIdAsync(request.UserId);

        exercise.TryGetDidacticMaterialRating(request.UserId, out var rating);
        var didacticMaterialIsRateable = userResult.IsT0 && rating is null;

        return new Success<ExerciseDto>(new ExerciseDto(exercise.Name, exercise.Description, exercise.Author.UserName,
            exercise.Solutions.Select(s =>
                {
                    s.TryGetDidacticMaterialRating(request.UserId, out var usersRating);

                    return new ExerciseSolutionDto(s.Id, s.Author.UserName, s.CreatedOn.DateTime, s.AverageRating,
                        usersRating?.Rating ?? 0);
                }
            ),
            exercise.AverageRating, exercise.GetLastRatings(5), rating?.Rating ?? 0, didacticMaterialIsRateable,
            exercise.Comments.Select(c => new OpinionDto(c.CreatedOn.DateTime, c.Author.UserName, c.Comment))));
    }
}