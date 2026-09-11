using Gym.BusinessLogic.DTOs.HealthRecords;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class HealthRecordService(IRepository<HealthyRecord> healthRecordRepository) : IHealthRecordService
{
    public async Task<HealthRecordDto?> GetByMemberIdAsync(
        int memberId,
        CancellationToken cancellationToken = default)
    {
        var healthRecord = await healthRecordRepository.FindAsync(
            record => record.MemberId == memberId,
            cancellationToken);

        if (healthRecord is null)
        {
            return null;
        }

        return new HealthRecordDto
        {
            Height = healthRecord.HeightInCentimeters,
            Weight = healthRecord.WeightInKilograms,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };
    }
}
