using EducationalPlatform.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Domain.Abstractions.Services;

public interface IAzureBlobStorageService
{
    Task<BlobDto> GetBlobByNameAsync(Guid blobName);
    Task UploadBlobAsync(Guid blobName, IFormFile file);
}