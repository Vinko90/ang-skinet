using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    
    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _repo.GetProductsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _repo.GetProductByIdAsync(id);
    }
}
