using Gym.BusinessLogic.Repositories;
using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton.Bookings;

namespace Gym.DataAccess.Repositories;

internal sealed class BookingRepository(GymDbContext context) : Repository<Booking>(context), IBookingRepository
{
    public Task<IReadOnlyList<Booking>> GetTodayWithDetailsAsync(
        CancellationToken cancellationToken = default)
    {
        return GetAllWithSpecificationAsync(
            new BookingWithMemberSessionDetails(),
            cancellationToken);
    }
}
