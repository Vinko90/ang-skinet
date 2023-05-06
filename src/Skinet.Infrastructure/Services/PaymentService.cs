using Microsoft.Extensions.Configuration;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Stripe;
using Product = Skinet.Core.Entities.Product;

namespace Skinet.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IBasketRedisRepository _basketRepo;
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;

    public PaymentService(IBasketRedisRepository basketRepo, IUnitOfWork uow, IConfiguration config)
    {
        _basketRepo = basketRepo;
        _uow = uow;
        _config = config;
    }
    
    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecKey"];

        var basket = await _basketRepo.GetBasketAsync(basketId);
        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _uow.Repository<DeliveryMethod>()
                .GetByIdAsync((int)basket.DeliveryMethodId);

            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var prodItem = await _uow.Repository<Product>()
                .GetByIdAsync(item.Id);
            if (item.Price != prodItem.Price)
            {
                item.Price = prodItem.Price;
            }
        }

        var service = new PaymentIntentService();
        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long) basket.Items.Sum(i => i.Quantity * i.Price * 100) + (long) shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> {"card"}
            };
            var intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long) basket.Items.Sum(i => i.Quantity * i.Price * 100) + (long) shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepo.UpdateBasketAsync(basket);
        return basket;
    }
}
