using EducationalPlatform.Application.Contracts.Exercise;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.GetExercise;

public record GetExerciseQuery(
    Guid Id,
    Guid? UserId
) : IRequest<OneOf<Success<ExerciseDto>, NotFound>>;