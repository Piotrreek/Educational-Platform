using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Commands.CreateExercise;

public record CreateExerciseCommand(
    string Name,
    IFormFile ExerciseFile,
    string? Description,
    Guid AuthorId
) : IRequest<OneOf<Success, BadRequestResult, ServiceUnavailableResult>>;