namespace EducationalPlatform.Domain.Exceptions;

public class EmailAlreadyConfirmedException : EducationalPlatformException
{
    public EmailAlreadyConfirmedException(string message) : base(message)
    {
    }
}