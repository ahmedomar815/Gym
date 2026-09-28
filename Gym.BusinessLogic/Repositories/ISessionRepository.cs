using Gym.DataAccess.Models;

namespace Gym.BusinessLogic.Repositories;

public interface ISessionRepository : IRepository<Session>
{
    Task<Session?> GetWithDetailsAsync(
        int sessionId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Session>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}
