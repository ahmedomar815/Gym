using Gym.BusinessLogic.DTOs.Dashboard;

namespace Gym.BusinessLogic.Services;

public interface IDashboardService
{
    Task<DashboardDto> GetSummaryAsync(CancellationToken cancellationToken = default);
}
