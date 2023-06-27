namespace EducationalPlatform.Domain;

public static class ErrorMessages
{
    public static string AccountAlreadyConfirmedErrorMessage => "You have already confirmed your account!";

    public static string BadAccountConfirmationLinkMessage =>
        "Account activation link is invalid or token has expired! Try to generate a new one in account panel.";
}