using Microsoft.Extensions.DependencyInjection;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Repositories;
using Skinet.Infrastructure.Services;

namespace Skinet.Infrastructure.Extensions;

public static class ServicesExt
{
    public static void AddRepositoriesAndServices(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRedisRepository, BasketRedisRepository>();
        
        //Services
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
    }
}
