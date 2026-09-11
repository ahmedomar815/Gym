using Gym.BusinessLogic.DTOs.HealthRecords;

namespace Gym.BusinessLogic.Services;

public interface IHealthRecordService
{
    Task<HealthRecordDto?> GetByMemberIdAsync(int memberId, CancellationToken cancellationToken = default);
}
