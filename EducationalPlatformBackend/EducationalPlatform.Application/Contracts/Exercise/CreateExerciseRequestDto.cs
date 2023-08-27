using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.Contracts.Exercise;

public class CreateExerciseRequestDto
{
    public string Name { get; set; } = string.Empty;
    public IFormFile ExerciseFile { get; set; } = null!;
    public string? Description { get; set; }
}