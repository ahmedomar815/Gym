using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Repositories;

internal sealed class SessionRepository(GymDbContext context) : Repository<Session>(context), ISessionRepository
{
    private readonly GymDbContext _context = context;

    public async Task<IReadOnlyList<Session>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .Include(session => session.Trainer)
            .Include(session => session.Category)
            .Include(session => session.Bookings)
            .ToListAsync(cancellationToken);
    }
}
