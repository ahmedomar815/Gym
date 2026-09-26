namespace Gym.BusinessLogic.Services;

using Gym.BusinessLogic.DTOs.Bookings;
using Gym.BusinessLogic.Results;

public interface IBookingService
{
    Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<BookingListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BookingFormDto> GetCreateFormAsync(CancellationToken cancellationToken = default);
    Task<Result> CreateAsync(CreateBookingDto model, CancellationToken cancellationToken = default);
}
