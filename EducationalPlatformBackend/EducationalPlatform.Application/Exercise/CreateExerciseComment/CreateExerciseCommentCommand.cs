using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseComment;

public record CreateExerciseCommentCommand
    (string Comment, Guid ExerciseId, Guid AuthorId) : IRequest<OneOf<Success<IReadOnlyCollection<ExerciseComment>>, BadRequestResult>>;