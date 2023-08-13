namespace EducationalPlatform.Application.Contracts.DidacticMaterial;

public class DetailedDidacticMaterialDto : DidacticMaterialDto
{
    public string? Description { get; }
    public string Academy { get; }
    public string Faculty { get; }
    public string Subject { get; }
    public string Course { get; }
    public bool Rateable { get; }
    public decimal? UsersRate { get; }
    public IEnumerable<decimal> LastRatings { get; }
    public IEnumerable<DidacticMaterialOpinionDto> Opinions { get; }

    public DetailedDidacticMaterialDto(Guid id, string name, string author, decimal averageRating, string? description,
        string academy, string faculty, string subject, string course, IEnumerable<decimal> lastRatings,
        IEnumerable<DidacticMaterialOpinionDto> opinions, bool rateable, decimal? usersRate) : base(id, name,
        author, averageRating)
    {
        Description = description;
        Academy = academy;
        Faculty = faculty;
        Subject = subject;
        Course = course;
        LastRatings = lastRatings;
        Opinions = opinions;
        Rateable = rateable;
        UsersRate = usersRate;
    }
}