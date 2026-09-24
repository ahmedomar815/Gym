using Gym.DataAceess.Data.Identity;
using Gym.DataAceess.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAceess.Data.Seeder;

public static class IdentitySeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser>
        userManager ,RoleManager<ApplicationRole> roleManager,IOptions<IdentitySeedOptions> options)
    {
        var seedUsers = options.Value;
        await EnsureRoleExist(roleManager, IdentityRolesNames.SuperAdmin, "Super Admin");

        await EnsureRoleExist(roleManager, IdentityRolesNames.SuperAdmin, " Admin");

        await EnsureRoleExist( roleManager,  IdentityRolesNames.Admin, "Admin");

        await EnsureUserAsync(userManager, seedUsers.SuperAdmin, "Super Administrator", IdentityRolesNames.SuperAdmin);





    }

    private static async Task EnsureUserAsync(  UserManager<ApplicationUser> userManager,  AdminSeedOptions userOptions,
    string fullName,
    string role)
    {
        var user = await userManager.FindByEmailAsync(userOptions.Email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = userOptions.Email,
                Email = userOptions.Email,
                FullName = fullName,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                user,
                userOptions.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(
                    $"Failed to create {role} user: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            var result = await userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(
                    $"Failed to assign role '{role}': {errors}");
            }
        }
    }

    private static async Task EnsureRoleExist(RoleManager<ApplicationRole> roleManager,string roleName,string displayName)
    {
      if(!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager.CreateAsync(new ApplicationRole { Name = roleName, DisplayName = displayName, Id = Guid.NewGuid() });
            if (!result.Succeeded)
                throw new InvalidOperationException("faild to create role");
        }

     
    }
}
