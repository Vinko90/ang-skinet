using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Core.Entities.Identity;
using Skinet.Infrastructure.Identity;

namespace Skinet.Infrastructure.Extensions;

public static class IdentityExt
{
    public static void AddIdentityPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("IdentityConnection"), 
                b =>
                {
                    b.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName);
                });
        });

        services.AddIdentityCore<AppUser>(opt =>
            {
                //Add identity options here
                opt.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddSignInManager<SignInManager<AppUser>>();
    }
}
