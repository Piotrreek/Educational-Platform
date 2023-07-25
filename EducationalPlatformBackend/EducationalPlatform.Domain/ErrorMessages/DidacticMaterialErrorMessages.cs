namespace EducationalPlatform.Domain.ErrorMessages;

public static class DidacticMaterialErrorMessages
{
    public static string MaterialWithIdNotExists =>
        "Didactic material with this id does not exist!";

    public static string BadRatingValue
        => "Rating value must be between 0 and 5!";

    public static string CannotSetContent =>
        "Content of didactic material cannot be set, if chosen didactic material type is file!";

    public static string FileCannotBeEmpty =>
        "File cannot be empty!";

    public static string ContentCannotBeEmpty =>
        "Content cannot be empty!";

    public static string NameCannotBeEmpty =>
        "Name of didactic material cannot be empty!";
}