using Gym.BusinessLogic.DTOs.Sessions;
using Gym.BusinessLogic.Results;


namespace Gym.BusinessLogic.Services;

public interface ISessionService
{
    Task<IReadOnlyList<SessionListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateSessionDto model, CancellationToken cancellationToken = default);
}
