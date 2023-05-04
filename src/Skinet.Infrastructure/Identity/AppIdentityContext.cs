using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities.Identity;

namespace Skinet.Infrastructure.Identity;

public class AppIdentityContext : IdentityDbContext<AppUser>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
        : base(options)
    {
    }
}
