using Gym.DataAccess.Models;

namespace Gym.BusinessLogic.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IReadOnlyList<Booking>> GetTodayWithDetailsAsync(
        CancellationToken cancellationToken = default);
}
