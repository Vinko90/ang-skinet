using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Extensions;

public static class PersistenceExt
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"), 
                b =>
            {
                b.MigrationsAssembly(typeof(StoreContext).Assembly.FullName);
            });
        },ServiceLifetime.Transient);
    }
}
