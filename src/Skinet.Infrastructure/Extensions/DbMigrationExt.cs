using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Extensions;

public static class DbMigrationExt
{
    public static async void MigrateDatabase(this IHost webApp)
    {
        using var scope = webApp.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
        try
        {
            await appContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during migrations: {ex.Message}");
        }
    }
}
