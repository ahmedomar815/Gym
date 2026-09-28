using Gym.BusinessLogic.Repositories;
using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton.Memberships;

namespace Gym.DataAccess.Repositories;

internal sealed class MembershipRepository(GymDbContext context)
    : Repository<Membership>(context), IMembershipRepository
{
    public Task<IReadOnlyList<Membership>> GetAllWithMemberAndPlanAsync(
        CancellationToken cancellationToken = default)
    {
        return GetAllWithSpecificationAsync(
            new MembershipWithMemberAndPlanSpecification(),
            cancellationToken);
    }
}
