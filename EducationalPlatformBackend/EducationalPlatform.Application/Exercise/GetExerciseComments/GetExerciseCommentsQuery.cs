using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;

namespace EducationalPlatform.Application.Exercise.GetExerciseComments;

public record GetExerciseCommentsQuery(Guid ExerciseId) : IRequest<OneOf<IEnumerable<OpinionDto>, BadRequestResult>>;