namespace Gym.Presentation.ViewModels.Sessions;

public sealed class DeleteSessionViewModel
{
    public int Id { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string TrainerName { get; init; } = string.Empty;

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init; }

    public int Capacity { get; init; }

    public int CountBooking { get; init; }
}
