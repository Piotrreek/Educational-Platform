using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolution;

public record CreateExerciseSolutionCommand(IFormFile SolutionFile, Guid ExerciseId, Guid AuthorId) : IRequest<
    OneOf<Success<IReadOnlyCollection<ExerciseSolutionDto>>, BadRequestResult, ServiceUnavailableResult>>;