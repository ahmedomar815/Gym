namespace Gym.BusinessLogic.DTOs.Memberships;

public sealed class MembershipListItemDto
{
    public int Id { get; init; }
    public int MemberId { get; init; }
    public string MemberName { get; init; } = string.Empty;
    public string PlanName { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
}
