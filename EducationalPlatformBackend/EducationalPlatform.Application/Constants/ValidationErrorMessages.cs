namespace EducationalPlatform.Application.Constants;

public static class ValidationErrorMessages
{
    public static string FieldNotEmptyMessage(string fieldName) =>
        $"{fieldName} must not be empty!";

    public static string EmailFormatMessage =>
        "Email must be valid!";

    public static string PasswordLengthMessage =>
        "Password length must be greater or equal to 8";

    public static string PasswordFormatMessage =>
        "Password has to consist of at least: one capital letter, one small letter, one digit and one special sign";

    public static string ValuesEqualMessage(string firstField, string secondField) =>
        $"Value of {firstField} and {secondField} must be identical!";

    public static string PhoneNumberFormatMessage =>
        "Phone number must be valid!";

    public static string WrongType => "Given type is wrong!";

    public static string PdfFileError => "Uploaded file must be PDF file!";
}