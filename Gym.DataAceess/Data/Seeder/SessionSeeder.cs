using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder;

public static class SessionSeeder
{
    private const string CategoryName = "General Fitness";
    private const string TrainerEmail = "seed.trainer@example.com";
    private const string EndedSessionDescription = "Completed fitness session for Ahmed 92";
    private const string UpcomingSessionDescription = "Upcoming fitness session for Ahmed 43";
    private const string Ahmed1SessionDescription = "Upcoming fitness session for Ahmed 1";

    public static async Task SeedAsync(GymDbContext gymDbContext)
    {
        var category = await gymDbContext.Categories
            .SingleOrDefaultAsync(category => category.Name == CategoryName);

        if (category is null)
        {
            category = new Category
            {
                Name = CategoryName,
                Description = "General gym and fitness sessions."
            };
            gymDbContext.Categories.Add(category);
            await gymDbContext.SaveChangesAsync();
        }

        var trainer = await gymDbContext.Trainers
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(trainer => trainer.Email == TrainerEmail);

        if (trainer is null)
        {
            trainer = new Trainer
            {
                Name = "Seed Trainer",
                DateOfBirth = new DateOnly(1988, 5, 10),
                Email = TrainerEmail,
                PhoneNumber = "+201700000000",
                Gender = Gender.Male,
                Address = new Address { City = "Cairo", Street = "1 Fitness Street" },
                IsActive = true,
                CategoryId = category.Id,
                Speciality = Speciality.GeneralFitness
            };
            gymDbContext.Trainers.Add(trainer);
            await gymDbContext.SaveChangesAsync();
        }

        var members = await gymDbContext.Members
            .Where(member => member.Email == "ahmed92@gmail.com"
                          || member.Email == "ahmed43@gmail.com"
                          || member.Email == "ahmed1@gmail.com")
            .ToDictionaryAsync(member => member.Email);

        if (members.Count != 3)
            return;

        var now = DateTime.UtcNow;
        var endedSession = await GetOrCreateSessionAsync(
            gymDbContext, EndedSessionDescription, now.AddDays(-2), trainer.Id, category.Id);
        var upcomingSession = await GetOrCreateSessionAsync(
            gymDbContext, UpcomingSessionDescription, now.AddDays(2), trainer.Id, category.Id);
        var ahmed1Session = await GetOrCreateSessionAsync(
            gymDbContext, Ahmed1SessionDescription, now.AddDays(3), trainer.Id, category.Id);

        await AddBookingIfMissingAsync(gymDbContext, members["ahmed92@gmail.com"].Id, endedSession.Id);
        await AddBookingIfMissingAsync(gymDbContext, members["ahmed43@gmail.com"].Id, upcomingSession.Id);
        await AddBookingIfMissingAsync(gymDbContext, members["ahmed1@gmail.com"].Id, ahmed1Session.Id);
        await gymDbContext.SaveChangesAsync();
    }

    private static async Task<Session> GetOrCreateSessionAsync(
        GymDbContext gymDbContext, string description, DateTime startTime, int trainerId, int categoryId)
    {
        var session = await gymDbContext.Sessions
            .SingleOrDefaultAsync(session => session.Description == description);

        if (session is not null)
            return session;

        session = new Session
        {
            Description = description,
            Capacity = 10,
            StartTime = startTime,
            EndTime = startTime.AddHours(1),
            TrainerId = trainerId,
            CategoryId = categoryId
        };
        gymDbContext.Sessions.Add(session);
        await gymDbContext.SaveChangesAsync();
        return session;
    }

    private static async Task AddBookingIfMissingAsync(GymDbContext gymDbContext, int memberId, int sessionId)
    {
        if (await gymDbContext.Bookings.AnyAsync(booking => booking.MemberId == memberId && booking.SessionId == sessionId))
            return;

        gymDbContext.Bookings.Add(new Booking { MemberId = memberId, SessionId = sessionId });
    }
}
