namespace EducationalPlatform.Application.Contracts.Academy.UniversitySubject;

public class CreateUniversitySubjectRequestDto
{
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectDegree { get; set; } = string.Empty;
    public Guid FacultyId { get; set; } = Guid.Empty;
}