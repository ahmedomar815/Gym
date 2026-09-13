using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class CategorySeeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        if (await dbContext.Categories.AnyAsync())
        {
            return;
        }

        await dbContext.Categories.AddRangeAsync(
        [
            new Category
            {
                Name = "Strength Training",
                Description = "Resistance and strength-building group workouts."
            },
            new Category
            {
                Name = "Yoga",
                Description = "Yoga sessions focused on mobility, balance, and recovery."
            }
        ]);

        await dbContext.SaveChangesAsync();
    }
}
