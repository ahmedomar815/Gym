namespace Gym.Presentation.ViewModels.Memberships;

public sealed class MembershipListItemViewModel
{
    public int Id { get; init; }
    public int MemberId { get; init; }
    public string MemberName { get; init; } = string.Empty;
    public string PlanName { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
}
