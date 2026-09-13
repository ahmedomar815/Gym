using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Repositories;

internal sealed class BookingRepository(GymDbContext context) : Repository<Booking>(context), IBookingRepository
{
    private readonly GymDbContext _context = context;

    public Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default)
    {
        return _context.Bookings.AnyAsync(booking => booking.MemberId == memberId, cancellationToken);
    }
}
