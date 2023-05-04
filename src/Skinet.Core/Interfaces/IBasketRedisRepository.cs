using Skinet.Core.Entities;

namespace Skinet.Core.Interfaces;

public interface IBasketRedisRepository
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}
