namespace EducationalPlatform.Application.Contracts.Academy.UniversitySubject;

public class CreateUniversitySubjectRequestDto
{
    public string SubjectName { get; set; }
    public string SubjectDegree { get; set; }
    public Guid UniversityId { get; set; }
    public Guid FacultyId { get; set; }

    public CreateUniversitySubjectRequestDto()
    {
        SubjectName = string.Empty;
        SubjectDegree = string.Empty;
        UniversityId = Guid.Empty;
        FacultyId = Guid.Empty;
    }
}