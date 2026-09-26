using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAceess.Specificaiton.Bookings;

public sealed class BookingWithMemberSessionDetails : Specification<Booking>
{
    public BookingWithMemberSessionDetails()
    {
        AddInclude(query => query
            .Include(booking => booking.Member)
            .Include(booking => booking.Session)
                .ThenInclude(session => session.Category)
            .Include(booking => booking.Session)
                .ThenInclude(session => session.Trainer));

        AddOrderBy(query => query
            .OrderBy(booking => booking.Session.StartTime)
            .ThenBy(booking => booking.Member.Name));
    }
}
