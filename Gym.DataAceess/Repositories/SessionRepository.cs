using Gym.BusinessLogic.Repositories;
using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton;
using Gym.DataAceess.Specificaiton.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Repositories;

internal sealed class SessionRepository(GymDbContext context) : Repository<Session>(context), ISessionRepository
{
    private readonly GymDbContext _context = context;

    public async Task<IReadOnlyList<Session>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(_context.Sessions.AsQueryable(), new SessionWithTrainerCategoryAndBooking())
            .ToListAsync(cancellationToken);
    }

    public Task<Session?> GetWithDetailsAsync(
        int sessionId,
        CancellationToken cancellationToken = default)
    {
        return GetEntityWithSpecificationAsync(
            new SessionWithTrainerCategoryAndBookingById(sessionId),
            cancellationToken);
    }
}
