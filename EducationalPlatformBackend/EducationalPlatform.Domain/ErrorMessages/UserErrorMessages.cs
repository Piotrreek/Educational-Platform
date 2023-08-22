namespace EducationalPlatform.Domain.ErrorMessages;

public static class UserErrorMessages
{
    public static string UserWithIdNotExists
        => "User with given id does not exist!";

    public static string WrongPassword
        => "Your old password is wrong!";
}