using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Exercise.CreateExerciseSolutionReview;

public record CreateExerciseSolutionReviewCommand(
    IFormFile? ReviewFile,
    string? ReviewContent,
    Guid AuthorId,
    Guid ExerciseSolutionId
) : IRequest<OneOf<Success, BadRequestResult, ServiceUnavailableResult>>;