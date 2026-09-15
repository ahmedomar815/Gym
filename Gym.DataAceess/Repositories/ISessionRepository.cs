using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

public interface ISessionRepository : IRepository<Session>
{
    Task<IReadOnlyList<Session>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}
