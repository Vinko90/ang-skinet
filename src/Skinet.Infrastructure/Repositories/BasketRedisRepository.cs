using System.Text.Json;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using StackExchange.Redis;

namespace Skinet.Infrastructure.Repositories;

public class BasketRedisRepository : IBasketRedisRepository
{
    private readonly IDatabase _redis;

    public BasketRedisRepository(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }
    
    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var data = await _redis.StringGetAsync(basketId);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _redis.StringSetAsync(basket.Id, 
            JsonSerializer.Serialize(basket), 
            TimeSpan.FromDays(10));

        if (!created) return null;

        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _redis.KeyDeleteAsync(basketId);
    }
}
