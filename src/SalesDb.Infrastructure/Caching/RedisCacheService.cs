using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SalesDb.Application.Interfaces;

namespace SalesDb.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    public RedisCacheService(IDistributedCache cache) => _cache = cache;
    public async Task<T?> GetAsync<T>(string key)
    {
        var s = await _cache.GetStringAsync(key);
        return s == null ? default : JsonSerializer.Deserialize<T>(s);
    }
    public Task SetAsync<T>(string key, T value, TimeSpan? ttl = null) =>
        _cache.SetStringAsync(key, JsonSerializer.Serialize(value),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = ttl ?? TimeSpan.FromMinutes(5) });
    public Task RemoveAsync(string key) => _cache.RemoveAsync(key);
}