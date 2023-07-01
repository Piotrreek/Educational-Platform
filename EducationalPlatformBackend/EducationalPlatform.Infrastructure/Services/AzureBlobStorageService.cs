using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EducationalPlatform.Application.Configuration;
using EducationalPlatform.Domain.Abstractions.Services;
using EducationalPlatform.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EducationPlatform.Infrastructure.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureBlobStorageConfiguration _blobStorageConfiguration;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient, IOptions<AzureBlobStorageConfiguration> options)
    {
        _blobServiceClient = blobServiceClient;
        _blobStorageConfiguration = options.Value;
    }


    public async Task<BlobDto> GetBlobByNameAsync(Guid blobName)
    {
        var container = await GetContainerClientAsync();
        var blob = container.GetBlobClient(blobName.ToString());
        var data = await blob.OpenReadAsync();
        var downloadResult = await blob.DownloadContentAsync();

        return new BlobDto(data, downloadResult.Value.Details.ContentType);
    }

    public async Task UploadBlobAsync(Guid blobName, IFormFile file)
    {
        var container = await GetContainerClientAsync();
        var blob = container.GetBlobClient(blobName.ToString());
        await using var data = file.OpenReadStream();
        await blob.UploadAsync(data, new BlobHttpHeaders()
        {
            ContentType = file.ContentType,
            ContentDisposition = file.ContentDisposition,
        });
    }

    private async Task<BlobContainerClient> GetContainerClientAsync()
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobStorageConfiguration.ContainerName);
        await blobContainerClient.CreateIfNotExistsAsync();

        return blobContainerClient;
    }
}