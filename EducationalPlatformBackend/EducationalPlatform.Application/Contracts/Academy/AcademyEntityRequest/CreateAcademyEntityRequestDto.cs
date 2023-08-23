namespace EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;

public class CreateAcademyEntityRequestDto
{
    public string EntityType { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? AdditionalInformation { get; set; }
    public string? UniversitySubjectDegree { get; set; }
    public string? UniversityCourseSession { get; set; }
    public Guid? UniversityId { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? UniversitySubjectId { get; set; }
}