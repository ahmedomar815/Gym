using Gym.BusinessLogic.DTOs.Plans;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Mapster;

namespace Gym.BusinessLogic.Services;

internal sealed class PlanService(IRepository<Plan> planRepository) : IPlanService
{
    public async Task<IReadOnlyList<PlanDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var plans = await planRepository.GetAllAsync(cancellationToken);
        return plans.Adapt<List<PlanDto>>();
    }

    public async Task<PlanDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.GetByIdAsync(id, cancellationToken);
        return plan?.Adapt<PlanDto>();
    }
}
