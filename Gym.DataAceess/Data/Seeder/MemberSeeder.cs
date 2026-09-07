using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class MemberSeeder
{
    public static async Task SeedAsync(GymDbContext gymDbContext)
    {
        if (await gymDbContext.Members.AnyAsync())
            return;

        var members = new List<Member>
        {
            new Member
            {
                FirstName = "Omar",
                LastName = "Hassan",
                DateOfBirth = new DateOnly(1995, 4, 12),
                Email = "omar.hassan@example.com",
                PhoneNumber = "+201001234567",
                Gender = Gender.Male,
                Address = new Address { City = "Cairo", Street = "15 Tahrir Street" },
                IsActive = true,
                JoinDate = new DateTime(2026, 1, 10),
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 178m,
                    WeightInKilograms = 76.5m,
                    BloodType = BloodType.OPositive
                }
            },
            new Member
            {
                FirstName = "Mariam",
                LastName = "Adel",
                DateOfBirth = new DateOnly(1998, 9, 23),
                Email = "mariam.adel@example.com",
                PhoneNumber = "+201109876543",
                Gender = Gender.Female,
                Address = new Address { City = "Giza", Street = "8 Nile Corniche" },
                IsActive = true,
                JoinDate = new DateTime(2026, 2, 1),
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 165m,
                    WeightInKilograms = 61m,
                    BloodType = BloodType.APositive
                }
            },
            new Member
            {
                FirstName = "Youssef",
                LastName = "Samir",
                DateOfBirth = new DateOnly(1992, 11, 5),
                Email = "youssef.samir@example.com",
                PhoneNumber = "+201203456789",
                Gender = Gender.Male,
                Address = new Address { City = "Alexandria", Street = "22 Saad Zaghloul Street" },
                IsActive = true,
                JoinDate = new DateTime(2026, 3, 15),
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 182m,
                    WeightInKilograms = 84m,
                    BloodType = BloodType.BPositive
                }
            }
        };

        await gymDbContext.Members.AddRangeAsync(members);
        await gymDbContext.SaveChangesAsync();
    }
}
