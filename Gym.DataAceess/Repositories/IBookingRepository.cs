using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;

namespace Gym.DataAceess.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<bool> HasBookingsForMemberAsync(int memberId, CancellationToken cancellationToken = default);
}
