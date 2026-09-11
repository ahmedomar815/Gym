using Gym.BusinessLogic.DTOs.Plans;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class PlanService(IRepository<Plan> planRepository) : IPlanService
{
    public async Task<IReadOnlyList<PlanDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var plans = await planRepository.GetAllAsync(cancellationToken);
        return plans.Select(ToDto).ToList();
    }

    public async Task<PlanDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.GetByIdAsync(id, cancellationToken);
        return plan is null ? null : ToDto(plan);
    }

    private static PlanDto ToDto(Plan plan) => new()
    {
        Id = plan.Id,
        Name = plan.Name,
        Description = plan.Description,
        DurationInDays = plan.DurationInDays,
        Price = plan.Price,
        IsActive = plan.IsActive
    };
}
