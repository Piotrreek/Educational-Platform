namespace EducationalPlatform.Application.Contracts.Academy.AssignUser;

public class AssignUserToAcademyEntitiesRequestDto
{
    public Guid? UniversityId { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? UniversitySubject { get; set; }
}