namespace EducationalPlatform.Domain.ErrorMessages;

public class UniversitySubjectErrorMessages
{
    public static string EmptyName =>
        "Subject name cannot be empty!";
    
    public static string UserAlreadyAssignedToSubject =>
        "You are already assigned to subject! You have to leave it first.";
    
    public static string UserAlreadyInSameSubject =>
        "You are already assigned to this subject!";

    public static string UserNotInUniversitySubject =>
        "You are not assigned to this subject!";
}