using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Repositories;

internal class MemberRepository(GymDbContext context) : Repository<Member>(context), IMemberRepository
{
    private readonly GymDbContext _context = context;

    public async Task<bool> IsEmailTakenAsync(string normalizedEamil, CancellationToken cancellationToken,int ?id = null)
    {
        return await _context.Members.AnyAsync(m => m.Email==normalizedEamil && (!id.HasValue || m.Id != id), cancellationToken);
    }

    public async Task<bool> IsPhoneTakenAsync(string phone, CancellationToken cancellationToken, int? id = null)
    {
        return await _context.Members.AnyAsync(m => m.PhoneNumber== phone&&(!id.HasValue||m.Id!=id ), cancellationToken);
    }

    public async Task<Member?> GetByIdWithMembershipsAndPlanAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Members
            .Include(member => member.Memberships)
            .ThenInclude(membership => membership.Plan)
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);
    }

}
