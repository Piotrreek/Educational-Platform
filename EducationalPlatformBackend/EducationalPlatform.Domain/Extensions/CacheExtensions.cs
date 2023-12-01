using Microsoft.Extensions.Caching.Memory;

namespace EducationalPlatform.Domain.Extensions;

public static class CacheExtensions
{
    public static readonly MemoryCacheEntryOptions DefaultCacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(360))
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetPriority(CacheItemPriority.Normal);
}