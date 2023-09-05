namespace EducationalPlatform.Application.Contracts;

public class OpinionDto
{
    public DateTime CreatedOn { get; }
    public string Author { get; }
    public string Opinion { get; }

    public OpinionDto(DateTime createdOn, string author, string opinion)
    {
        CreatedOn = createdOn;
        Author = author;
        Opinion = opinion;
    }
}