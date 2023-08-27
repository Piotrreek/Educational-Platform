using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseComment;

public class CreateExerciseCommentCommandHandler : IRequestHandler<CreateExerciseCommentCommand,
    OneOf<Success<IReadOnlyCollection<ExerciseComment>>, BadRequestResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseCommentCommandHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
    {
        _userRepository = userRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<OneOf<Success<IReadOnlyCollection<ExerciseComment>>, BadRequestResult>> Handle(
        CreateExerciseCommentCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.AuthorId);

        if (userResult.IsT1)
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        var exerciseResult = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);

        if (!exerciseResult.TryPickT0(out var exercise, out _))
        {
            return new BadRequestResult(ExerciseErrorMessages.ExerciseWithIdNotExists);
        }

        var result = exercise.AddComment(request.Comment, request.AuthorId);

        if (!result.TryPickT0(out var comments, out var badRequest))
        {
            return badRequest;
        }

        return new Success<IReadOnlyCollection<ExerciseComment>>(comments);
    }
}