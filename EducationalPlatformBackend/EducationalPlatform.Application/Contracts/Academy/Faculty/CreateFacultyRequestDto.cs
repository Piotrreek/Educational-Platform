namespace EducationalPlatform.Application.Contracts.Academy.Faculty;

public class CreateFacultyRequestDto
{
    public string FacultyName { get; set; }
    public Guid UniversityId { get; set; }

    public CreateFacultyRequestDto()
    {
        FacultyName = string.Empty;
        UniversityId = Guid.Empty;
    }
}