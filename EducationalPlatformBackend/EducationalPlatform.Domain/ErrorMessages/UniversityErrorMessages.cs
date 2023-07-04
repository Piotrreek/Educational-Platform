namespace EducationalPlatform.Domain.ErrorMessages;

public static class UniversityErrorMessages
{
    public static string UserAlreadyInSameUniversity =>
        "You are already assigned to this university!";

    public static string UserAlreadyAssignedToUniversity =>
        "You are already assigned to university! You have to leave it first.";
    
    public static string EmptyName =>
        "Course name cannot be empty!";
    
    public static string UserNotInUniversity =>
        "You are not assigned to this university!";
}