namespace EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;

public class GroupedCreateAcademyEntityRequestDto
{
    public string EntityType { get; }
    public IEnumerable<GetCreateAcademyEntityRequestDto> Requests { get; }

    public GroupedCreateAcademyEntityRequestDto(string entityType,
        IEnumerable<GetCreateAcademyEntityRequestDto> requests)
    {
        EntityType = entityType;
        Requests = requests;
    }
}