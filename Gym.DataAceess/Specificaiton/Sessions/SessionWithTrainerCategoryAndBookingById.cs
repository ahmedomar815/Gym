using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace Gym.DataAceess.Specificaiton.Sessions;

public  class SessionWithTrainerCategoryAndBookingById:Specification<Session>
{
    public SessionWithTrainerCategoryAndBookingById(int id)
    {

        Criteria= session => session.Id == id;

        AddInclude(query => query
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .Include(s => s.Bookings));
    }

}
