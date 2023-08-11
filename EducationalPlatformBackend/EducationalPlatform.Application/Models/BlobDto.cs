namespace EducationalPlatform.Application.Models;

public class BlobDto
{
    public byte[] Data { get; }
    public string ContentType { get; }
    public string FileName { get; set; } = null!;

    public BlobDto(byte[] data, string contentType)
    {
        Data = data;
        ContentType = contentType;
    }
}