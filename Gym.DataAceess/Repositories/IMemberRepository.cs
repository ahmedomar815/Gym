using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

public  interface IMemberRepository : IRepository<Member>
{
     Task<bool> IsEmailTakenAsync(string normalizedEamil, CancellationToken cancellationToken, int? id = null);
    Task<bool> IsPhoneTakenAsync(string phone, CancellationToken cancellationToken, int? id = null);
}
