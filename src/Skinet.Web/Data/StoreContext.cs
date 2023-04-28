using Microsoft.EntityFrameworkCore;
using Skinet.Web.Entities;

namespace Skinet.Web.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
