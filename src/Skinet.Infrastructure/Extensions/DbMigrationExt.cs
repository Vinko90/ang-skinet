using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skinet.Core.Entities.Identity;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Identity;

namespace Skinet.Infrastructure.Extensions;

public static class DbMigrationExt
{
    public static async void MigrateDatabases(this IHost webApp)
    {
        using var scope = webApp.Services.CreateScope();
        await using var storeDbContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
        await using var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityContext>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        try
        {
            await storeDbContext.Database.MigrateAsync();
            await identityDbContext.Database.MigrateAsync();
            
            await StoreContextSeed.SeedAsync(storeDbContext);
            await AppIdentityContextSeed.SeedAsync(userManager);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during migrations: {ex.Message}");
        }
    }
}
