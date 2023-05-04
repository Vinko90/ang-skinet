using Microsoft.AspNetCore.Identity;
using Skinet.Core.Entities.Identity;

namespace Skinet.Infrastructure.Identity;

public static class AppIdentityContextSeed
{
    public static async Task SeedAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Bob",
                Email = "bob@t.com",
                UserName = "bob@t.com",
                Address = new Address
                {
                    FirstName = "Bob",
                    LastName = "Bobbity",
                    Street = "10 For The Street",
                    City = "New York",
                    State = "NY",
                    ZipCode = "90210"
                }
            };

            await userManager.CreateAsync(user, "Dummy_123!");
        }
    }
}
