using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

public  interface IMemberRepository : IRepository<Member>
{
    Task<bool> IsPhoneTakenAsync(string normalizedEamil, CancellationToken cancellationToken);
    Task<bool> IsEmailTakenAsync(string normalizedEamil, CancellationToken cancellationToken);
    Task<Member?> GetByIdWithMembershipsAndPlanAsync(int id, CancellationToken cancellationToken = default);

}
