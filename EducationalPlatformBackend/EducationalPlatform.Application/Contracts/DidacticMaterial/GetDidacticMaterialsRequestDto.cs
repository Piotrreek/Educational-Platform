namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class GetDidacticMaterialsRequestDto
{
    public Guid? UniversityId { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? UniversitySubjectId { get; set; }
    public Guid? UniversityCourseId { get; set; }
}