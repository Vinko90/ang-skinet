using Microsoft.Extensions.DependencyInjection;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Repositories;
using Skinet.Infrastructure.Services;

namespace Skinet.Infrastructure.Extensions;

public static class ReposExt
{
    public static void AddRepositories(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBasketRedisRepository, BasketRedisRepository>();
        
        //TokenService
        services.AddScoped<ITokenService, TokenService>();
    }
}
