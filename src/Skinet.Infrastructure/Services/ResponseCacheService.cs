using System.Text.Json;
using Skinet.Core.Interfaces;
using StackExchange.Redis;

namespace Skinet.Infrastructure.Services;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDatabase _db;

    public ResponseCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }
    
    public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    {
        if (response == null) return;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var serializedResponse = JsonSerializer.Serialize(response, options);

        await _db.StringSetAsync(cacheKey, serializedResponse, timeToLive);
    }

    public async Task<string> GetCachedResponseAsync(string cacheKey)
    {
        var cachedResponse = await _db.StringGetAsync(cacheKey);

        if (cachedResponse.IsNullOrEmpty) return null;
        return cachedResponse;
    }
}
