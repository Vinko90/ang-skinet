using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;

namespace Skinet.Infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductBrand> ProductBrands { get; set; }
    
    public DbSet<ProductType> ProductTypes { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName != "Microsoft.EntityFrameworkCore.Sqlite") return;
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(decimal));

            foreach (var prop in properties)
            {
                modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
            }
        }
    }
}
