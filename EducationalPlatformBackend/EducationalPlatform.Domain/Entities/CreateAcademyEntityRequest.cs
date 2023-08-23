using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.Primitives;

namespace EducationalPlatform.Domain.Entities;

public class CreateAcademyEntityRequest : Entity
{
    public string EntityTypeName { get; private set; } = null!;
    public string EntityName { get; private set; } = null!;
    public string? AdditionalInformation { get; private set; }
    public UniversitySubjectDegree? UniversitySubjectDegree { get; private set; }
    public UniversityCourseSession? UniversityCourseSession { get; private set; }
    public University? University { get; private set; }
    public Guid? UniversityId { get; private set; }
    public Faculty? Faculty { get; private set; }
    public Guid? FacultyId { get; private set; }
    public UniversitySubject? UniversitySubject { get; private set; }
    public Guid? UniversitySubjectId { get; private set; }
    public User Requester { get; private set; } = null!;
    public Guid RequesterId { get; private set; }


    public CreateAcademyEntityRequest(string entityName, User requester,
        string? additionalInformation = null)
    {
        EntityName = entityName;
        Requester = requester;
        AdditionalInformation = additionalInformation;
        EntityTypeName = nameof(University);
    }

    public void AssignPropertiesForFacultyRequest(University university)
    {
        University = university;
        EntityTypeName = nameof(Faculty);
    }

    public void AssignPropertiesForUniversitySubjectRequest(University university, Faculty faculty,
        UniversitySubjectDegree universitySubjectDegree)
    {
        University = university;
        Faculty = faculty;
        UniversitySubjectDegree = universitySubjectDegree;
        EntityTypeName = nameof(UniversitySubject);
    }

    public void AssignPropertiesFoUniversityCourseRequest(University university, Faculty faculty,
        UniversitySubject universitySubject, UniversityCourseSession universityCourseSession)
    {
        University = university;
        Faculty = faculty;
        UniversitySubject = universitySubject;
        UniversityCourseSession = universityCourseSession;
        EntityTypeName = nameof(UniversityCourse);
    }

    // For EF
    private CreateAcademyEntityRequest()
    {
    }
}