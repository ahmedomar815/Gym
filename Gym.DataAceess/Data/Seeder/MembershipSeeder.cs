using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class MembershipSeeder
{
    public static async Task SeedAsync(GymDbContext gymDbContext)
    {
        var members = await gymDbContext.Members
            .Where(member => member.Email == "omar.hassan@example.com"
                          || member.Email == "mariam.adel@example.com"
                          || member.Email == "youssef.samir@example.com")
            .ToDictionaryAsync(member => member.Email);

        var plans = await gymDbContext.Plans
            .Where(plan => plan.Name == "Standard"
                        || plan.Name == "Premium"
                        || plan.Name == "Annual")
            .ToDictionaryAsync(plan => plan.Name);

        if (members.Count != 3 || plans.Count != 3)
            return;

        var seededMembers = members.Values.ToList();
        var memberIdsWithMemberships = await gymDbContext.Memberships
            .Where(membership => seededMembers.Select(member => member.Id).Contains(membership.MemberId))
            .Select(membership => membership.MemberId)
            .Distinct()
            .ToListAsync();

        var startDate = DateTime.UtcNow.Date;
        var memberships = new[]
        {
            CreateMembership(members["omar.hassan@example.com"], plans["Standard"], startDate.AddDays(-15)),
            CreateMembership(members["mariam.adel@example.com"], plans["Premium"], startDate.AddDays(-20)),
            CreateMembership(members["youssef.samir@example.com"], plans["Annual"], startDate.AddDays(-45))
        }
        .Where(membership => !memberIdsWithMemberships.Contains(membership.MemberId))
        .ToList();

        if (memberships.Count == 0)
            return;

        await gymDbContext.Memberships.AddRangeAsync(memberships);
        await gymDbContext.SaveChangesAsync();
    }

    private static Membership CreateMembership(Member member, Plan plan, DateTime startDate) => new()
    {
        MemberId = member.Id,
        PlanId = plan.Id,
        StartDate = startDate,
        EndDate = startDate.AddDays(plan.DurationInDays)
    };
}
