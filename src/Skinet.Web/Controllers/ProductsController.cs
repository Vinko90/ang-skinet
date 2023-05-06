using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.DTO;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Web.Errors;
using Skinet.Web.Helpers;

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
    [Cached(60)]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductParamsDto productParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpec(productParams);
        var countSpec = new ProductWithFiltersForCountSpec(productParams);

        var totalItems = await _productsRepo.CountAsync(countSpec);
        var products = await _productsRepo.GetAsync(spec);
        
        var data = _mapper.Map<IReadOnlyList<Product>, List<ProductDto>>(products);

        return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id:int}")]
    [Cached(60)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var prod = await _productsRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpec(id));
        return _mapper.Map<Product, ProductDto>(prod);
    }
    
    [HttpGet("brands")]
    [Cached(60)]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _prodBrandsRepo.GetAllAsync());
    }
    
    [HttpGet("types")]
    [Cached(60)]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        return Ok(await _prodTypeRepo.GetAllAsync());
    }
}
