using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class TrainerSeeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        if (await dbContext.Trainers.AnyAsync())
        {
            return;
        }

        var strengthCategory = await dbContext.Categories
            .SingleAsync(category => category.Name == "Strength Training");
        var yogaCategory = await dbContext.Categories
            .SingleAsync(category => category.Name == "Yoga");

        await dbContext.Trainers.AddRangeAsync(
        [
            new Trainer
            {
                Name = "Omar Hassan",
                Email = "omar.hassan@gym.local",
                PhoneNumber = "01010000001",
                DateOfBirth = new DateTime(1990, 4, 15),
                Gender = Gender.Male,
                IsActive = true,
                Speciality = Speciality.StrengthTraining,
                CategoryId = strengthCategory.Id,
                Address = new Address { BuidingNumber = 12, Street = "Tahrir Street", City = "Cairo" }
            },
            new Trainer
            {
                Name = "Sara Adel",
                Email = "sara.adel@gym.local",
                PhoneNumber = "01010000002",
                DateOfBirth = new DateTime(1993, 9, 22),
                Gender = Gender.Female,
                IsActive = true,
                Speciality = Speciality.Yoga,
                CategoryId = yogaCategory.Id,
                Address = new Address { BuidingNumber = 8, Street = "Zamalek Street", City = "Cairo" }
            }
        ]);

        await dbContext.SaveChangesAsync();
    }
}
