namespace EducationalPlatform.Domain;

public static class ErrorMessages
{
    public static string AccountAlreadyConfirmedErrorMessage => "You have already confirmed your account!";

    public static string BadAccountConfirmationLinkMessage =>
        "Account activation link is invalid or expired! Try to generate a new one in account panel.";

    public static string BadResetPasswordLinkMessage =>
        "Reset password link is invalid or expired! Try to generate a new one in account panel.";

    public static string UserAlreadyInSameUniversity =>
        "You are already assigned to this university!";

    public static string UserAlreadyAssignedToUniversity =>
        "You are already assigned to university! You have to leave it first.";

    public static string FacultyNameEmptyErrorMessage =>
        "Faculty name cannot be empty!";
    
    public static string SubjectNameEmptyErrorMessage =>
        "Subject name cannot be empty!";

    public static string UniversityCourseNameEmptyErrorMessage =>
        "Course name cannot be empty!";
    
    public static string UserAlreadyAssignedToFaculty =>
        "You are already assigned to faculty! You have to leave it first.";
    
    public static string UserAlreadyInSameFaculty =>
        "You are already assigned to this faculty!";
    
    public static string UserAlreadyAssignedToSubject =>
        "You are already assigned to subject! You have to leave it first.";
    
    public static string UserAlreadyInSameSubject =>
        "You are already assigned to this subject!";
}