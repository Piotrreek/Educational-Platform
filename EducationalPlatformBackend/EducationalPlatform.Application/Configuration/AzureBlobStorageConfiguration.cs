namespace EducationalPlatform.Application.Configuration;

public class AzureBlobStorageConfiguration
{
    public string Container { get; set; }
    public string ContainerName { get; set; }

    public AzureBlobStorageConfiguration()
    {
        Container = string.Empty;
        ContainerName = string.Empty;
    }
}