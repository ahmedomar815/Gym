using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

internal sealed class BookingRepository(GymDbContext context) : Repository<Booking>(context), IBookingRepository
{
}
