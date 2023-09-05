using EducationalPlatform.Application.Models;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.GetExerciseFile;

public record GetExerciseFileQuery(Guid Id) : IRequest<OneOf<NotFound, BlobDto>>;