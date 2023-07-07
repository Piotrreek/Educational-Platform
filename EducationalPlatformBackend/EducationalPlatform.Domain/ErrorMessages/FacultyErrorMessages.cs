namespace EducationalPlatform.Domain.ErrorMessages;

public static class FacultyErrorMessages
{
    public static string EmptyName =>
        "Faculty name cannot be empty!";
    
    public static string UserAlreadyAssignedToFaculty =>
        "You are already assigned to faculty! You have to leave it first.";
    
    public static string UserAlreadyInSameFaculty =>
        "You are already assigned to this faculty!";
    
    public static string FacultyWithNameAlreadyExists =>
        "Faculty with this name already exists in this university!";
    
    public static string UserNotInFaculty =>
        "You are not assigned to this faculty!";

    public static string FacultyInUniversityNotExists =>
        "Faculty with this id does not exist in this university";
}