using Skinet.Core.Entities;

namespace Skinet.Core.Interfaces;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
}
