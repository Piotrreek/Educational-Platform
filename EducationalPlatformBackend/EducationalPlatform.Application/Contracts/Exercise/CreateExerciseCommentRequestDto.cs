using System.Text.Json.Serialization;

namespace EducationalPlatform.Application.Contracts.Exercise;

public class CreateExerciseCommentRequestDto
{
    [JsonPropertyName("opinion")] public string Comment { get; set; } = string.Empty;
}