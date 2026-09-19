using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services;

internal sealed class BookingService(IUniteOfWork unitOfWork) : IBookingService
{
    public Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default)
    {
        return unitOfWork.Bookings.HasBookingsForMemberAsync(memberId, cancellationToken);
    }
}
