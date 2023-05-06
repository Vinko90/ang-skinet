using Skinet.Core.Entities.OrderAggregate;

namespace Skinet.Core.Specifications;

public class OrderByPaymentIntentIdSpec : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpec(string paymentIntentId) 
        : base(o => o.PaymentIntentId == paymentIntentId)
    {
    }
}
