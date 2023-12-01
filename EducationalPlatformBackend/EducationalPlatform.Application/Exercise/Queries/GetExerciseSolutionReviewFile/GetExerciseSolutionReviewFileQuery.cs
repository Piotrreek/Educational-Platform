using EducationalPlatform.Application.Models;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.Queries.GetExerciseSolutionReviewFile;

public record GetExerciseSolutionReviewFileQuery(
    Guid ReviewId
) : IRequest<OneOf<Success<BlobDto>, NotFound, ServiceUnavailableResult>>;