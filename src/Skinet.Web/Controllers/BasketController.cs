using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.DTO;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRedisRepository _basketRepo;
    private readonly IMapper _mapper;

    public BasketController(IBasketRedisRepository basketRepo, IMapper mapper)
    {
        _basketRepo = basketRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await _basketRepo.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
        var updated = await _basketRepo.UpdateBasketAsync(customerBasket);
        return Ok(updated);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await _basketRepo.DeleteBasketAsync(id);
    }
}
