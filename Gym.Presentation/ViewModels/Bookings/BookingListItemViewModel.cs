namespace Gym.Presentation.ViewModels.Bookings;

public sealed class BookingListItemViewModel
{
    public int Id { get; init; }
    public string MemberName { get; init; } = string.Empty;
    public string SessionName { get; init; } = string.Empty;
    public string TrainerName { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}
