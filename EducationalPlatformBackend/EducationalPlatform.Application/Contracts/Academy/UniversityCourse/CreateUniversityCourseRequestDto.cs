namespace EducationalPlatform.Application.Contracts.Academy.UniversityCourse;

public class CreateUniversityCourseRequestDto
{
    public string CourseName { get; set; }
    public string CourseSession { get; set; }
    public Guid UniversityId { get; set; }
    public Guid FacultyId { get; set; }
    public Guid UniversitySubjectId { get; set; }

    public CreateUniversityCourseRequestDto()
    {
        CourseName = string.Empty;
        CourseSession = string.Empty;
        UniversityId = Guid.Empty;
        FacultyId = Guid.Empty;
        UniversitySubjectId = Guid.Empty;
    }
}