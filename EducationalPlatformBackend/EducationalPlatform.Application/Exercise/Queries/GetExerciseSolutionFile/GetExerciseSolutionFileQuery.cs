using EducationalPlatform.Application.Models;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionFile;

public record GetExerciseSolutionFileQuery(Guid Id) : IRequest<OneOf<NotFound, BlobDto>>;