using Gym.DataAccess.Models.Enums;

namespace Gym.BusinessLogic.DTOs.HealthRecords;

public sealed class HealthRecordDto
{
    public decimal Height { get; init; }

    public decimal Weight { get; init; }

    public BloodType BloodType { get; init; }

    public string? Note { get; init; }
}
