using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Web.DTO;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _prodBrandsRepo;
    private readonly IGenericRepository<ProductType> _prodTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> prodBrandsRepo, 
        IGenericRepository<ProductType> prodTypeRepo,
        IMapper mapper)
    {
        _productsRepo = productsRepo;
        _prodBrandsRepo = prodBrandsRepo;
        _prodTypeRepo = prodTypeRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var prods = await _productsRepo.GetAsync(new ProductsWithTypesAndBrandsSpec());
        return _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(prods);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var prod = await _productsRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpec(id));
        return _mapper.Map<Product, ProductDto>(prod);
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _prodBrandsRepo.GetAllAsync());
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        return Ok(await _prodTypeRepo.GetAllAsync());
    }
}
