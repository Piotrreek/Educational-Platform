using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.GetExerciseComments;

public class GetExerciseCommentsQueryHandler : IRequestHandler<GetExerciseCommentsQuery,
    OneOf<IEnumerable<OpinionDto>, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseCommentsQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<IEnumerable<OpinionDto>, BadRequestResult>> Handle(GetExerciseCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseWithIdNotExists);
        }

        return exercise.Comments.Select(c => new OpinionDto(c.CreatedOn.DateTime, c.Author.UserName, c.Comment))
            .ToList();
    }
}