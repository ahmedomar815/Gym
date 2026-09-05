using Gym.DataAccess.Models.Enums;

namespace Gym.DataAccess.Models;

public class HealthyRecord : BaseEntity
{
    public decimal HeightInCentimeters { get; set; }
    public decimal WeightInKilograms { get; set; }
    public BloodType BloodType { get; set; }
 

    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;
}
