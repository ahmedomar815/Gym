using Gym.DataAccess.Models;

namespace Gym.BusinessLogic.Repositories;

public  interface IMemberRepository : IRepository<Member>
{
    Task<Member?> GetWithMembershipsAndPlanAsync(
        int memberId,
        CancellationToken cancellationToken = default);

     Task<bool> IsEmailTakenAsync(string normalizedEamil, CancellationToken cancellationToken, int? id = null);
    Task<bool> IsPhoneTakenAsync(string phone, CancellationToken cancellationToken, int? id = null);
    Task<bool> HasBookingsAsync(int memberId, CancellationToken cancellationToken = default);
}
