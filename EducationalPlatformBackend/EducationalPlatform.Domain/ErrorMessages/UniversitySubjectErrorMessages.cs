namespace EducationalPlatform.Domain.ErrorMessages;

public static class UniversitySubjectErrorMessages
{
    public static string IdCannotBeEmpty
        => "Id of subject cannot be empty!";

    public static string EmptyName =>
        "Subject name cannot be empty!";

    public static string SubjectWithNameAlreadyExists =>
        "Subject with this name already exists in this faculty!";

    public static string CannotAssignUniversitySubjectWithoutFacultyOrUniversityBeingSetEarlier =>
        "University subject cannot be set without setting university and faculty earlier!";

    public static string SubjectInFacultyOrUniversityNotExists =>
        "Subject with this Id does not exist in given university or faculty!";

    public static string UniversitySubjectWithIdNotExists =>
        "Subject with this id does nto exist!";
}