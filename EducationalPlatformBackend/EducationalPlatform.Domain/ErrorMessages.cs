namespace EducationalPlatform.Domain;

public static class ErrorMessages
{
    public static string AccountAlreadyConfirmedErrorMessage => "You have already confirmed your account!";

    public static string BadAccountConfirmationLinkMessage =>
        "Account activation link is invalid or expired! Try to generate a new one in account panel.";

    public static string BadResetPasswordLinkMessage =>
        "Reset password link is invalid or expired! Try to generate a new one in account panel.";
}