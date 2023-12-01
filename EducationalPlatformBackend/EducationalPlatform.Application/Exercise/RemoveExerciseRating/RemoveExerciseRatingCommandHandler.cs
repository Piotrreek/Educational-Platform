using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.RemoveExerciseRating;

internal sealed class RemoveExerciseRatingCommandHandler : RatingHandler<Domain.Entities.Exercise, ExerciseRating>,
    IRequestHandler<RemoveExerciseRatingCommand, OneOf<Success<RatingDto>, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public RemoveExerciseRatingCommandHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository) :
        base(userRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<Success<RatingDto>, BadRequestResult>> Handle(RemoveExerciseRatingCommand request,
        CancellationToken cancellationToken)
    {
        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseWithIdNotExists);
        }

        return await RemoveRating(exercise, request.UserId);
    }
}