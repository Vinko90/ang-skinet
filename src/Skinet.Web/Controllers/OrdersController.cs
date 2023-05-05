using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.DTO;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Web.Errors;

namespace Skinet.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
        var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
        if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order!"));
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var orders = await _orderService.GetOrdersForUserAsync(email);
        return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var order = await _orderService.GetOrderByIdAsync(id, email);
        if (order == null) return NotFound(new ApiResponse(404));
        return _mapper.Map<OrderToReturnDto>(order);
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        return Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}
