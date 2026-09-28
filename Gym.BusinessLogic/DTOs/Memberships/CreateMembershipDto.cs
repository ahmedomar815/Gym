namespace Gym.BusinessLogic.DTOs.Memberships;

public sealed class CreateMembershipDto
{
    public int MemberId { get; init; }
    public int PlanId { get; init; }
    public DateTime StartDate { get; init; }
}
