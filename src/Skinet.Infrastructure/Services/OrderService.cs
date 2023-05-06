using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;

namespace Skinet.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;
    private readonly IBasketRedisRepository _basketRepo;

    public OrderService(IUnitOfWork uow, IBasketRedisRepository basketRepo)
    {
        _uow = uow;
        _basketRepo = basketRepo;
    }
    
    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        //Get basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);
        
        //Get items from the prod repo
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _uow.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        //Get delivery method from the repo
        var deliveryMethod = await _uow.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        
        //Calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        
        //Check if order exist
        var spec = new OrderByPaymentIntentIdSpec(basket.PaymentIntentId);
        var order = await _uow.Repository<Order>().GetEntityWithSpec(spec);

        if (order != null)
        {
            order.ShipToAddress = shippingAddress;
            order.DeliveryMethod = deliveryMethod;
            order.Subtotal = subtotal;
            _uow.Repository<Order>().Update(order);
        }
        else
        {
            //Create order
            order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
            _uow.Repository<Order>().Add(order);
        }
        
        //Save to db
        var dbResult = await _uow.Complete();
        
        return dbResult <= 0 ? null : order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpec(buyerEmail);
        return await _uow.Repository<Order>().GetAsync(spec);
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpec(id, buyerEmail);
        return await _uow.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _uow.Repository<DeliveryMethod>().GetAllAsync();
    }
}
