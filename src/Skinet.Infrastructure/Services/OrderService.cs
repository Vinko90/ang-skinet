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

        //Create order
        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

        //Save to db
        _uow.Repository<Order>().Add(order);
        var dbResult = await _uow.Complete();
        if (dbResult <= 0) return null; //No Changes saved to db
        
        //delete basket
        await _basketRepo.DeleteBasketAsync(basketId);
        
        //return order
        return order;
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
