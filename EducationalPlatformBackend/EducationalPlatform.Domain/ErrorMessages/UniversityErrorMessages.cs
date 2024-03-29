namespace EducationalPlatform.Domain.ErrorMessages;

public static class UniversityErrorMessages
{
    public static string IdCannotBeEmpty => "Id of university cannot be empty!";

    public static string UniversityWithNameAlreadyExists =>
        "University with this name already exists!";

    public static string UniversityWithIdNotExists =>
        "University with this id does not exist!";
}