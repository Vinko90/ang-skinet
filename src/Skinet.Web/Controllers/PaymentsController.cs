using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Web.Errors;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _payService;

    public PaymentsController(IPaymentService payService)
    {
        _payService = payService;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _payService.CreateOrUpdatePaymentIntent(basketId);

        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket!"));
        return basket;
    }
}
