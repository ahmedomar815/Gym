using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Gym.DataAceess.Specificaiton;
using Gym.DataAceess.Specificaiton.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Repositories;

internal sealed class SessionRepository(GymDbContext context) : Repository<Session>(context), ISessionRepository
{
    private readonly GymDbContext _context = context;

    public async Task<IReadOnlyList<Session>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(_context.Sessions.AsQueryable(), new SessionWithTrainerCategoryAndBooking())
            .ToListAsync(cancellationToken);
    }
}
