using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class PlanSeeder
{
    public static async Task SeedAsync(GymDbContext gymDbContext)
    {
        bool hasAnyPlans = await gymDbContext.Plans.AnyAsync();

        if (hasAnyPlans)
            return;

        var plans = new List<Plan>
        {
            new Plan
            {
                Name = "Basic",
                Description = "Basic gym membership with access to gym facilities.",
                DurationInDays = 30,
                Price = 500m,
                IsActive = true
            },
            new Plan
            {
           
                Name = "Standard",
                Description = "Standard membership with gym access and group classes.",
                DurationInDays = 90,
                Price = 1200m,
                IsActive = true
            },
            new Plan
            {
                
                Name = "Premium",
                Description = "Premium membership with full access and personal training.",
                DurationInDays = 180,
                Price = 2000m,
                IsActive = true
            },
            new Plan
            {
                
                Name = "Annual",
                Description = "Annual membership with all gym facilities and services.",
                DurationInDays = 365,
                Price = 3500m,
                IsActive = true
            }
        };

        await gymDbContext.Plans.AddRangeAsync(plans);
        await gymDbContext.SaveChangesAsync();
    }
}
