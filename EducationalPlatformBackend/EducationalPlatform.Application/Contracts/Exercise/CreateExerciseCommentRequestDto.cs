namespace EducationalPlatform.Application.Contracts.Exercise;

public class CreateExerciseCommentRequestDto
{
    public string Comment { get; set; } = string.Empty;
    public Guid ExerciseId { get; set; }
}