namespace Gym.BusinessLogic.DTOs.Bookings;

public sealed class BookingFormDto
{
    public IReadOnlyList<BookingMemberOptionDto> Members { get; init; } = [];
    public IReadOnlyList<BookingSessionOptionDto> Sessions { get; init; } = [];
}

public sealed class BookingMemberOptionDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public sealed class BookingSessionOptionDto
{
    public int Id { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string TrainerName { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int Capacity { get; init; }
    public int BookedCount { get; init; }
}
