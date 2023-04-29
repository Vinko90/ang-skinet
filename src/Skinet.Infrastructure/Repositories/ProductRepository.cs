using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _db;
    
    public ProductRepository(StoreContext db)
    {
        _db = db;
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _db.Products.AsNoTracking()
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _db.Products.AsNoTracking()
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _db.ProductBrands.AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _db.ProductTypes.AsNoTracking().ToListAsync();
    }
}
