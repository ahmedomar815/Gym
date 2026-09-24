using Gym.DataAceess.Data.Identity;
using Gym.DataAceess.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAceess.Data.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(UserManager<ApplicationUser>userManager,RoleManager<ApplicationRole> roleManager, IOptions<IdentitySeedOptions> options)
    {
        await IdentitySeeder.SeedAsync(userManager, roleManager, options);
    }
}
