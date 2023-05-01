using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _prodBrandsRepo;
    private readonly IGenericRepository<ProductType> _prodTypeRepo;

    public ProductsController(
        IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> prodBrandsRepo, 
        IGenericRepository<ProductType> prodTypeRepo)
    {
        _productsRepo = productsRepo;
        _prodBrandsRepo = prodBrandsRepo;
        _prodTypeRepo = prodTypeRepo;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await _productsRepo.GetAsync(new ProductsWithTypesAndBrandsSpec()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _productsRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpec(id));
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _prodBrandsRepo.GetAllAsync());
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _prodTypeRepo.GetAllAsync());
    }
}
