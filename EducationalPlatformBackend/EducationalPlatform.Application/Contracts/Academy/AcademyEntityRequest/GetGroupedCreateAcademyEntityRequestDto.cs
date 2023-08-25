namespace EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;

public class GetGroupedCreateAcademyEntityRequestDto
{
    public string EntityType { get; }
    public IEnumerable<GetCreateAcademyEntityRequestDto> Requests { get; }

    public GetGroupedCreateAcademyEntityRequestDto(string entityType,
        IEnumerable<GetCreateAcademyEntityRequestDto> requests)
    {
        EntityType = entityType;
        Requests = requests;
    }
}