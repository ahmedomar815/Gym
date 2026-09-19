using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace Gym.DataAceess.Specificaiton.Sessions;

public class SessionWithTrainerCategoryAndBooking : Specification<Session>
{
    public SessionWithTrainerCategoryAndBooking()
    {
        

        AddInclude(query => query
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .Include(s => s.Bookings));
    }
}