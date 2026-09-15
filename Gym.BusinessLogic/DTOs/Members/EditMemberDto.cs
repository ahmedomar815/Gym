namespace Gym.BusinessLogic.DTOs.Members;

public sealed class EditMemberDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public int BuildingNumber { get; init; }

    public string City { get; init; } = string.Empty;

    public string Street { get; init; } = string.Empty;

    public string? PhotoUrl { get; init; }
}
