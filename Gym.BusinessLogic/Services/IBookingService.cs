namespace Gym.BusinessLogic.Services;

public interface IBookingService
{
    Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default);
}
