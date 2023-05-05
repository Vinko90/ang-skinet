using System.Reflection;
using System.Text.Json;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;

namespace Skinet.Infrastructure.Data;

public static class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext db)
    {
        var seedFiles = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Data", "Seeds");
        
        if (!db.ProductBrands.Any())
        {
            var brandsData = await File.ReadAllTextAsync(Path.Combine(seedFiles, "brands.json"));
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            db.ProductBrands.AddRange(brands);
        }
        
        if (!db.ProductTypes.Any())
        {
            var typesData = await File.ReadAllTextAsync(Path.Combine(seedFiles, "types.json"));
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            db.ProductTypes.AddRange(types);
        }
        
        if (!db.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync(Path.Combine(seedFiles, "products.json"));
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            db.Products.AddRange(products);
        }
        
        if (!db.DeliveryMethods.Any())
        {
            var deliveryMethodsData = await File.ReadAllTextAsync(Path.Combine(seedFiles, "delivery.json"));
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
            db.DeliveryMethods.AddRange(deliveryMethods);
        }

        if (db.ChangeTracker.HasChanges())
            await db.SaveChangesAsync();
    }
}
