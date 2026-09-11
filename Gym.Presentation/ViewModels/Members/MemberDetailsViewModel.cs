namespace Gym.Presentation.ViewModels.Members;

public sealed class MemberDetailsViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? PhotoUrl { get; init; }

    public string Email { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string Gender { get; init; } = string.Empty;

    public DateOnly DateOfBirth { get; init; }

    public string Address { get; init; } = string.Empty;

    public string? PlanName { get; init; }

    public DateOnly? MembershipStartDate { get; init; }

    public DateOnly? MembershipEndDate { get; init; }
}
