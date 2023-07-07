namespace EducationalPlatform.Domain.ErrorMessages;

public static class UniversityCourseErrorMessages
{
    public static string EmptyName =>
        "Course name cannot be empty!";
    
    public static string CourseWithNameAlreadyExists =>
        "Course with this name already exists on this subject!";
}