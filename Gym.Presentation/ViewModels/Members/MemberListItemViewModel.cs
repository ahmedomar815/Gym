namespace Gym.Presentation.ViewModels.Members;

public sealed class MemberListItemViewModel
{
    public int Id { get; init; }

    public string? PhotoUrl { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Gender { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;
}
