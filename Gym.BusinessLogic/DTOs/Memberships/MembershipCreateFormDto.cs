namespace Gym.BusinessLogic.DTOs.Memberships;

public sealed class MembershipCreateFormDto
{
    public IReadOnlyList<MembershipMemberOptionDto> Members { get; init; } = [];
    public IReadOnlyList<MembershipPlanOptionDto> Plans { get; init; } = [];
}

public sealed class MembershipMemberOptionDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public sealed class MembershipPlanOptionDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int DurationInDays { get; init; }
    public decimal Price { get; init; }
}
