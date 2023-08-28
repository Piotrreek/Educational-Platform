using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseRating;

public class ExerciseRatingCommandHandler : RatingHandler<Domain.Entities.Exercise, ExerciseRating>,
    IRequestHandler<CreateExerciseRatingCommand, OneOf<Success<RatingDto>, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseRatingCommandHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository) :
        base(userRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<Success<RatingDto>, BadRequestResult>> Handle(CreateExerciseRatingCommand request,
        CancellationToken cancellationToken)
    {
        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseWithIdNotExists);
        }

        return await AddRating(exercise, request.UserId, request.Rating);
    }
}