namespace EducationalPlatform.Application.Contracts.Academy.University;

public class CreateUniversityRequestDto
{
    public string UniversityName { get; set; }

    public CreateUniversityRequestDto()
    {
        UniversityName = string.Empty;
    }
}