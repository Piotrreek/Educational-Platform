namespace EducationalPlatform.Domain.Models;

public class BlobDto
{
    public Stream Data { get; }
    public string ContentType { get; }

    public BlobDto(Stream data, string contentType)
    {
        Data = data;
        ContentType = contentType;
    }
}