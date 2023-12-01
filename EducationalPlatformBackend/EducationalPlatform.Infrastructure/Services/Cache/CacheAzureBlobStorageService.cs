using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using CacheExtensions = EducationalPlatform.Domain.Extensions.CacheExtensions;

namespace EducationPlatform.Infrastructure.Services.Cache;

public class CacheAzureBlobStorageService : IAzureBlobStorageService
{
    private const string GetBlobCacheKey = "blob-{0}";
    
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IMemoryCache _cache;

    public CacheAzureBlobStorageService(IAzureBlobStorageService azureBlobStorageService, IMemoryCache cache)
    {
        _azureBlobStorageService = azureBlobStorageService;
        _cache = cache;
    }

    public async Task<BlobDto> GetBlobByNameAsync(Guid blobName) 
    {
        var formattedCacheKey = string.Format(GetBlobCacheKey, blobName.ToString());
        if (_cache.TryGetValue(formattedCacheKey, out BlobDto? blob))
        {
            return blob!;
        }
        
        blob = await _azureBlobStorageService.GetBlobByNameAsync(blobName);

        _cache.Set(formattedCacheKey, blob, CacheExtensions.DefaultCacheEntryOptions);
        
        return blob;
    }

    public async Task UploadBlobAsync(Guid blobName, IFormFile file)
    {
        await _azureBlobStorageService.UploadBlobAsync(blobName, file);
    }
}