using Gym.DataAccess.Models;

namespace Gym.BusinessLogic.Repositories;

public interface IMembershipRepository : IRepository<Membership>
{
    Task<IReadOnlyList<Membership>> GetAllWithMemberAndPlanAsync(
        CancellationToken cancellationToken = default);
}
