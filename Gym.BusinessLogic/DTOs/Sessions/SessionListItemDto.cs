namespace Gym.BusinessLogic.DTOs.Sessions;

public sealed class SessionListItemDto
{
    public int Id { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string TrainerName { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int Capacity { get; init; }
    public int AvailableSlots { get; init; }
}
