namespace Gym.BusinessLogic.DTOs;

public sealed class MemberListItemDto
{
    public int Id { get; init; }

    public string? PhotoUrl { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Gender { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;
}
