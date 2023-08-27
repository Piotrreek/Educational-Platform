using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.Contracts.Exercise;

public class CreateExerciseSolutionReviewRequestDto
{
    public Guid ExerciseSolutionId { get; set; }
    public string? ReviewContent { get; set; }
    public IFormFile? ReviewFile { get; set; }
}