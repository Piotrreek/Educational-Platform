namespace EducationalPlatform.Application.Contracts.Academy.UniversityCourse;

public class CreateUniversityCourseRequestDto
{
    public string CourseName { get; set; } = string.Empty;
    public string CourseSession { get; set; } = string.Empty;
    public Guid UniversitySubjectId { get; set; } = Guid.Empty;
}