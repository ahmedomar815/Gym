namespace Gym.BusinessLogic.DTOs.Sessions;

public sealed class SessionDetailsDto
{
    public int Id { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string TrainerName { get; init; } = string.Empty;

    public int Capacity { get; init; }

    public int CountBooking { get; init; }

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init; }

   
}

