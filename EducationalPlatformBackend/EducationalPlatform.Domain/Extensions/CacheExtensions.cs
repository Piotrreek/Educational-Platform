using Microsoft.Extensions.Caching.Memory;

namespace EducationalPlatform.Domain.Extensions;

public static class CacheExtensions
{
    public static readonly MemoryCacheEntryOptions DefaultCacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(360))
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetPriority(CacheItemPriority.Normal);

    public static async Task<T> GetOrSaveAndGet<T>(this IMemoryCache cache, string cacheKey, Func<Task<T>> resolve)
    {
        if (!cache.TryGetValue(cacheKey, out T? value))
        {
            value = await resolve();
            cache.Set(cacheKey, value);
        }

        return value!;
    }
}