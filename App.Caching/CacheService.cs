using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching;

public class CacheService(IMemoryCache memoryCache): ICacheService
{
    public Task<T> GetAsync<T>(string key)
    {
        if(memoryCache.TryGetValue(key, out T cacheItem)) return Task.FromResult(cacheItem);
        return Task.FromResult(default(T));
    }

    public Task AddAsync<T>(string key, T value, TimeSpan? expiryTimeSpan)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expiryTimeSpan // cache de ne kadar kalacak optionsdan belirledik.
        };
        memoryCache.Set(key, value, cacheOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}