using EducationalPlatform.Application.Models;
using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.Abstractions.Services;

public interface IAzureBlobStorageService
{
    Task<BlobDto> GetBlobByNameAsync(Guid blobName);
    Task UploadBlobAsync(Guid blobName, IFormFile file);
}