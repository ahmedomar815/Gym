using Gym.BusinessLogic.DTOs.Plans;

namespace Gym.BusinessLogic.Services;

public interface IPlanService
{
    Task<IReadOnlyList<PlanDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PlanDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
