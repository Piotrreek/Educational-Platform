namespace EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;

public class GetCreateAcademyEntityRequestDto
{
    public Guid Id { get; }
    public string EntityType { get; set; }
    public string EntityName { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? SubjectDegree { get; set; }
    public string? CourseSession { get; set; }
    public string Status { get; set; }
    public Guid? UniversityId { get; set; }
    public string? UniversityName { get; set; }
    public Guid? FacultyId { get; set; }
    public string? FacultyName { get; set; }
    public Guid? SubjectId { get; set; }
    public string? SubjectName { get; set; }
    public Guid RequesterId { get; set; }
    public string RequesterName { get; set; }

    public GetCreateAcademyEntityRequestDto(string entityType, string entityName, string status, Guid requesterId,
        string requesterName, Guid id)
    {
        EntityType = entityType;
        EntityName = entityName;
        Status = status;
        RequesterId = requesterId;
        RequesterName = requesterName;
        Id = id;
    }
}