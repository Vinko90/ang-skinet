using Microsoft.Extensions.DependencyInjection;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Repositories;

namespace Skinet.Infrastructure.Extensions;

public static class ReposExt
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBasketRedisRepository, BasketRedisRepository>();
    }
}
