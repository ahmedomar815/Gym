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
                Name = "Omar Hassan",
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
                Name = "Mariam Adel",
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
                Name = "Youssef Samir",
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
            },
            new Member
            {
                Name = "Nour Khaled",
                DateOfBirth = new DateOnly(2000, 6, 18),
                Email = "nour.khaled@example.com",
                PhoneNumber = "+201312345678",
                Gender = Gender.Female,
                Address = new Address { City = "Cairo", Street = "42 Abbas El Akkad Street" },
                IsActive = true,
                JoinDate = new DateTime(2026, 4, 8),
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 168m,
                    WeightInKilograms = 63m,
                    BloodType = BloodType.ANegative
                }
            },
            new Member
            {
                Name = "Karim Mostafa",
                DateOfBirth = new DateOnly(1990, 1, 30),
                Email = "karim.mostafa@example.com",
                PhoneNumber = "+201412345678",
                Gender = Gender.Male,
                Address = new Address { City = "Giza", Street = "12 Sudan Street" },
                IsActive = true,
                JoinDate = new DateTime(2026, 5, 20),
                HealthyRecord = new HealthyRecord
                {
                    HeightInCentimeters = 175m,
                    WeightInKilograms = 80m,
                    BloodType = BloodType.ONegative
                }
            }
        };

        await gymDbContext.Members.AddRangeAsync(members);
        await gymDbContext.SaveChangesAsync();
    }
}
