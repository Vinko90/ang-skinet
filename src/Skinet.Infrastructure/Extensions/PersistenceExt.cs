using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Infrastructure.Data;
using StackExchange.Redis;

namespace Skinet.Infrastructure.Extensions;

public static class PersistenceExt
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //Store DB
        services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!, 
                b =>
            {
                b.MigrationsAssembly(typeof(StoreContext).Assembly.FullName);
            });
        },ServiceLifetime.Transient);
        
        //Add Redis
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var opt = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!);
            return ConnectionMultiplexer.Connect(opt);
        });
    }
}
