using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class MemberSeeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        if (await dbContext.Members.AnyAsync())
        {
            return;
        }

        await dbContext.Members.AddRangeAsync(
        [
            new Member
            {
                Name = "Ahmed Samir",
                Email = "ahmed.samir@gym.local",
                PhoneNumber = "01020000001",
                DateOfBirth = new DateTime(1996, 2, 10),
                Gender = Gender.Male,
                IsActive = true,
                JoinDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Address = new Address { BuidingNumber = 21, Street = "Nasr City Street", City = "Cairo" },
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 178m,
                    WeightInKilograms = 76m,
                    BloodType = BloodType.OPositive
                }
            },
            new Member
            {
                Name = "Nour Mohamed",
                Email = "nour.mohamed@gym.local",
                PhoneNumber = "01020000002",
                DateOfBirth = new DateTime(1998, 7, 18),
                Gender = Gender.Female,
                IsActive = true,
                JoinDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Address = new Address { BuidingNumber = 4, Street = "Maadi Street", City = "Cairo" },
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 165m,
                    WeightInKilograms = 60m,
                    BloodType = BloodType.APositive
                }
            }
        ]);

        await dbContext.SaveChangesAsync();
    }
}
