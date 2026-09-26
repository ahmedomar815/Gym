using System.ComponentModel.DataAnnotations;

namespace Gym.BusinessLogic.DTOs.Bookings;

public sealed class CreateBookingDto
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; init; }

    [Range(1, int.MaxValue)]
    public int SessionId { get; init; }
}
