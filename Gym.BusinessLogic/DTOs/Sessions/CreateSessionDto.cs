namespace Gym.BusinessLogic.DTOs.Sessions;

public sealed class CreateSessionDto
{
    public string Description { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int TrainerId { get; init; }
    public int CategoryId { get; init; }
}


