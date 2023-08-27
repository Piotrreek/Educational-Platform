using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.Contracts.Exercise;

public class CreateExerciseSolutionRequestDto
{
    public IFormFile SolutionFile { get; set; } = null!;
    public Guid ExerciseId { get; set; }
}