using EducationalPlatform.Application.Abstractions;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolutionRating;

public class CreateExerciseSolutionRatingCommandHandler : RatingHandler<ExerciseSolution, ExerciseSolutionRating>,
    IRequestHandler<CreateExerciseSolutionRatingCommand, OneOf<Success, BadRequestResult>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseSolutionRatingCommandHandler(IUserRepository userRepository,
        IExerciseRepository exerciseRepository) : base(userRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<Success, BadRequestResult>> Handle(CreateExerciseSolutionRatingCommand request,
        CancellationToken cancellationToken)
    {
        var solutionResult = await _exerciseRepository.GetExerciseSolutionByIdAsync(request.SolutionId);

        if (!solutionResult.TryPickT0(out var solution, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseSolutionWithIdNotExists);
        }

        await AddRating(solution, request.UserId, request.Rating);

        return new Success();
    }
}