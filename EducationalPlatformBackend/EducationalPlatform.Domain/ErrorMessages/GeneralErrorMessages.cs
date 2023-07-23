namespace EducationalPlatform.Domain.ErrorMessages;

public static class GeneralErrorMessages
{
    public static string AccountAlreadyConfirmedErrorMessage => "You have already confirmed your account!";

    public static string BadAccountConfirmationLinkMessage =>
        "Account activation link is invalid or expired! Try to generate a new one in account panel.";

    public static string BadResetPasswordLinkMessage =>
        "Reset password link is invalid or expired! Try to generate a new one in account panel.";

    public static string WrongUniversitySubjectDegreeConversion =>
        "Value of university subject degree is incorrect.";

    public static string WrongUniversityCourseSessionConversion =>
        "Value of university course session is incorrect.";

    public static string WrongDidacticMaterialTypeConversion 
        => "Value of didactic material type is wrong.";

    public static string WrongFacultyAndSubjectIds =>
        "Faculty or subject with given id does not exists or given subject is not in this faculty";
}