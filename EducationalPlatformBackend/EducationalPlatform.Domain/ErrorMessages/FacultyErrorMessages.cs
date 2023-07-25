namespace EducationalPlatform.Domain.ErrorMessages;

public static class FacultyErrorMessages
{
    public static string EmptyName =>
        "Faculty name cannot be empty!";

    public static string FacultyWithNameAlreadyExists =>
        "Faculty with this name already exists in this university!";

    public static string FacultyWithIdNotExists =>
        "Faculty with this id does not exist!";

    public static string CannotAssignFacultyWithoutUniversity =>
        "Faculty cannot be assigned without university being set earlier";
}