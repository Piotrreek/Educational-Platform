namespace EducationalPlatform.Domain.ErrorMessages;

public static class DidacticMaterialErrorMessages
{
    public static string BadRatingValue
        => "Rating value must be between 0 and 5!";

    public static string CannotSetContent =>
        "Content of didactic material cannot be set, if chosen didactic material type is file!";
}